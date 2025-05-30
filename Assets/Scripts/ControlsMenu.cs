using UnityEngine;
using UnityEngine.UI;

public class ControlsMenu : MonoBehaviour
{
    [Header("Menu UIs")]
    public GameObject desktopMenuUI;
    public GameObject mobileMenuUI;
    public GameObject VRMenuUI;
    [Header("Button UIs")]
    public GameObject desktopMenuButton;
    public GameObject mobileMenuButton;
    public GameObject VRMenuButton;
    [Header("Control Player Script")]
    public ControlPlayer controlPlayer = new ControlPlayer();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        desktopMenuUI.SetActive(true);
        mobileMenuUI.SetActive(true);
        VRMenuUI.SetActive(true);

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            OnDesktopMenu();
            Destroy(mobileMenuButton);
            Destroy(VRMenuButton);
        }
        else
        {
            OnMobileMenu();
            Destroy(desktopMenuButton);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDesktopMenu()
    {
        desktopMenuUI.SetActive(true);
        mobileMenuUI.SetActive(false);
        VRMenuUI.SetActive(false);
    }

    public void OnMobileMenu()
    {
        desktopMenuUI.SetActive(false);
        mobileMenuUI.SetActive(true);
        VRMenuUI.SetActive(false);
    }

    public void OnVRMenu()
    {
        desktopMenuUI.SetActive(false);
        mobileMenuUI.SetActive(false);
        VRMenuUI.SetActive(true);
    }
}
