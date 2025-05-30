using TMPro;
using UnityEngine;

public class Information : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject canvasFullUI;
    public GameObject canvasLeftUI;
    public GameObject canvasRightUI;
    [Header("Settings")]
    public bool isShowInformation = false;
    public string title = "Title";
    [TextArea]
    [SerializeField]
    public string description = "This is a description... :)";
    [Header("Control Player")]
    public ControlPlayer controlPlayer = new ControlPlayer();
    private GameObject informationMenuFull;
    private GameObject informationMenuLeft;
    private GameObject informationMenuRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isShowInformation = false;

        informationMenuFull = canvasFullUI.transform.Find("Information Menu").gameObject;
        informationMenuLeft = canvasLeftUI.transform.Find("Information Menu").gameObject;
        informationMenuRight = canvasRightUI.transform.Find("Information Menu").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (controlPlayer.gameControlMode == "Desktop" || controlPlayer.gameControlMode == "Mobile")
        {
            if (informationMenuFull.activeSelf == true) isShowInformation = true;
            else if (informationMenuFull.activeSelf == false) isShowInformation = false;
        }
        else if (controlPlayer.gameControlMode == "VR")
        {
            if (informationMenuLeft.activeSelf == true && informationMenuRight.activeSelf == true) isShowInformation = true;
            else if (informationMenuLeft.activeSelf == false && informationMenuRight.activeSelf == false) isShowInformation = false;
        }
    }

    public void onShowInformation()
    {
        if (controlPlayer.gameControlMode == "Desktop" || controlPlayer.gameControlMode == "Mobile")
        {
            if (isShowInformation == false)
            {
                informationMenuFull.GetComponent<InformationMenu>().ShowInformation();
                informationMenuFull.GetComponent<InformationMenu>().title = title;
                informationMenuFull.GetComponent<InformationMenu>().description = description;
            }
        }
        else if (controlPlayer.gameControlMode == "VR")
        {
            if (isShowInformation == false)
            {
                informationMenuLeft.GetComponent<InformationMenu>().ShowInformation();
                informationMenuLeft.GetComponent<InformationMenu>().title = title;
                informationMenuLeft.GetComponent<InformationMenu>().description = description;

                informationMenuRight.GetComponent<InformationMenu>().ShowInformation();
                informationMenuRight.GetComponent<InformationMenu>().title = title;
                informationMenuRight.GetComponent<InformationMenu>().description = description;
            }
        }
    }
}
