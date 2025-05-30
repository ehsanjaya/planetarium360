using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject canvasFullUI;
    public GameObject canvasLeftUI;
    public GameObject canvasRightUI;
    [Header("Tags")]
    public string grabbableTag = "Grabbable";
    public string clickableTag = "Clickable";
    [Header("Audio Source UI")]
    public AudioSource audioSource;
    public AudioResource audioResource;
    [Header("Control Player Script")]
    public ControlPlayer controlPlayer = new ControlPlayer();
    private InformationMenu informationMenuFull;
    private InformationMenu informationMenuLeft;
    private InformationMenu informationMenuRight;
    [Header("Key Controls Desktop")]
    public KeyBindingControl fireKeyBindDesktop;
    [Header("Key Controls VR")]
    public KeyBindingControl fireKeyBindVR;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasFullUI.SetActive(false);
        canvasLeftUI.SetActive(false);
        canvasRightUI.SetActive(false);

        informationMenuFull = canvasFullUI.transform.Find("Information Menu").GetComponent<InformationMenu>();
        informationMenuLeft = canvasLeftUI.transform.Find("Information Menu").GetComponent<InformationMenu>();
        informationMenuRight = canvasRightUI.transform.Find("Information Menu").GetComponent<InformationMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (
            Input.GetKeyDown(fireKeyBindDesktop.keyCode) && Time.timeScale == 1f && controlPlayer.gameControlMode == "Desktop" ||
            Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && controlPlayer.gameControlMode == "Mobile" ||
            Input.GetKeyDown(fireKeyBindVR.keyCode) && controlPlayer.gameControlMode == "VR"
        )
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                Ray ray = new Ray();  
                RaycastHit hit = new RaycastHit();

                if (controlPlayer.gameControlMode == "Desktop" || controlPlayer.gameControlMode == "VR") ray = new Ray(transform.position, transform.forward);
                else if (controlPlayer.gameControlMode == "Mobile") ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.transform.CompareTag(clickableTag))
                    {
                        if (hit.transform.GetComponent<Information>() != null)
                        {
                            hit.transform.GetComponent<Information>().onShowInformation();
                            audioSource.resource = audioResource;
                            audioSource.Play();
                        }
                    }
                }
            }
        }

        if (controlPlayer.gameControlMode == "Desktop" || controlPlayer.gameControlMode == "Mobile")
        {
            canvasFullUI.SetActive(true);
            canvasLeftUI.SetActive(false);
            canvasRightUI.SetActive(false);
        }
        else if (controlPlayer.gameControlMode == "VR")
        {
            canvasFullUI.SetActive(false);
            canvasLeftUI.SetActive(true);
            canvasRightUI.SetActive(true);
        }

        //for (int i = 0; i < 20; i++)
        //{
        //    KeyCode key = (KeyCode)((int)KeyCode.JoystickButton0 + i);
        //    if (Input.GetKeyDown(key))
        //    {
        //        text.text = key.ToString();
        //    }
        //}
    }
}
