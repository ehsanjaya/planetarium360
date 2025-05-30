using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationMenu : MonoBehaviour
{
    [Header("Title")]
    public string title = "Title";
    [Header("Description")]
    [TextArea]
    [SerializeField]
    public string description = "This is a description... :)";
    [Header("Information UIs")]
    public float speedScroll = 1f;
    public float defaultSpeedScroll = 1f;
    public TMP_Text titleUI;
    public TMP_Text descriptionUI;
    public GameObject informationUI;
    public Scrollbar scrollbar;
    [Header("Control Player Script")]
    public ControlPlayer controlPlayer = new ControlPlayer();
    [Header("Key Controls Desktop")]
    public KeyBindingControl closeKeyBindDesktop;
    [Header("Key Controls VR")]
    public KeyBindingControl closeKeyBindVR;
    [Header("Slider Controls VR")]
    public SliderControl scrollMultiplierSliderVR;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideInformation();
        scrollbar.value = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        titleUI.text = title;
        descriptionUI.text = description;

        scrollbar.value += Input.GetAxisRaw("Vertical") * speedScroll * Time.unscaledDeltaTime;
        scrollbar.value = Mathf.Clamp(scrollbar.value, 0f, 1f);

        if (controlPlayer.gameControlMode == "VR") speedScroll = scrollMultiplierSliderVR.value;
        else speedScroll = defaultSpeedScroll;

        if (Input.GetKeyDown(closeKeyBindDesktop.keyCode) && informationUI.activeSelf == true && controlPlayer.gameControlMode == "Desktop") HideInformation();
        if (Input.GetKeyDown(closeKeyBindVR.keyCode) && informationUI.activeSelf == true && controlPlayer.gameControlMode == "VR") HideInformation();
}
    public void HideInformation()
    {
        controlPlayer.mouseVisible = false;
        informationUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowInformation()
    {
        controlPlayer.mouseVisible = true;
        informationUI.SetActive(true);
        scrollbar.value = 1f;
        Time.timeScale = 0f;
    }
}
