using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Settings Mode")]
    public GameObject VRButtonUI;
    public GameObject MobileButtonUI;
    [Header("Control Player Script")]
    public ControlPlayer controlPlayer = new ControlPlayer();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        VRButtonUI.SetActive(false);
        MobileButtonUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (controlPlayer.gameControlMode == "Desktop")
        {
            VRButtonUI.SetActive(false);
            MobileButtonUI.SetActive(false);
        }
        else if (controlPlayer.gameControlMode == "Mobile" || controlPlayer.gameControlMode == "VR")
        {
            VRButtonUI.SetActive(true);
            MobileButtonUI.SetActive(true);
        }
    }
}
