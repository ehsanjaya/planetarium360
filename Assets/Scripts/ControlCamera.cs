using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class ControlCamera : MonoBehaviour
{
    [Header("Joystick Rotate")]
    [SerializeField] private FixedJoystick joystickRotate;
    [Header("Cameras")]
    [SerializeField] private GameObject cameraLeftObject;
    [SerializeField] private GameObject cameraRightObject;
    [SerializeField] private GameObject cameraFullObject;
    private Camera camera;
    [Header("Control Player Script")]
    public ControlPlayer controlPlayer = new ControlPlayer();
    [Header("Gyroscope")]
    private Vector2 turn = Vector2.zero;
    public GameObject cameraContainer;
    private bool gyroEnabled = false;
    private Gyroscope gyro;
    private Quaternion rot;
    [Header("VR Lens UI")]
    public GameObject VRLens;
    [Header("Crosshair UI")]
    public GameObject crosshair;
    [Header("Sensitivities")]
    public float mouseSensitivity = 10f;
    public float joystickSensitivity = 10f;
    public float VRSensitivity = 10f;
    [Header("Slider Controls Desktop")]
    public SliderControl mouseSensitivitySliderDesktop;
    [Header("Slider Controls Mobile")]
    public SliderControl joystickSensitivitySliderMobile;
    [Header("Slider Controls VR")]
    public SliderControl joystickSensitivitySliderVR;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main;
        camera.transform.localRotation = Quaternion.identity;

        if (controlPlayer.gameControlMode == "VR")
        {
            gyroEnabled = EnableGyro();
        }
        else
        {
            gyroEnabled = DisableGyro();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controlPlayer.gameControlMode == "Desktop")
        {
            mouseSensitivity = mouseSensitivitySliderDesktop.value;

            gyroEnabled = DisableGyro();

            VRLens.SetActive(false);
            cameraLeftObject.SetActive(false);
            cameraRightObject.SetActive(false);
            cameraFullObject.SetActive(true);
            crosshair.SetActive(true);

            if (controlPlayer.mouseVisible == false)
            {
                turn.x += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                turn.y += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            }

            transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0f);
        }
        else if (controlPlayer.gameControlMode == "Mobile")
        {
            joystickSensitivity = joystickSensitivitySliderMobile.value;

            gyroEnabled = DisableGyro();

            VRLens.SetActive(false);
            cameraLeftObject.SetActive(false);
            cameraRightObject.SetActive(false);
            cameraFullObject.SetActive(true);
            crosshair.SetActive(false);

            turn.x += joystickRotate.Horizontal * joystickSensitivity * Time.deltaTime;
            turn.y -= joystickRotate.Vertical * joystickSensitivity * Time.deltaTime;

            transform.rotation = Quaternion.Euler(turn.y, turn.x, 0f);
        }
        else if (controlPlayer.gameControlMode == "VR")
        {
            VRSensitivity = joystickSensitivitySliderVR.value;
            gyroEnabled = EnableGyro();

            VRLens.SetActive(true);
            cameraLeftObject.SetActive(true);
            cameraRightObject.SetActive(true);
            cameraFullObject.SetActive(false);
            crosshair.SetActive(false);

            if (gyroEnabled) transform.localRotation = Quaternion.Slerp(transform.localRotation, gyro.attitude * rot, VRSensitivity);
        }
    }
    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }

        return false;
    }

    private bool DisableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = false;

            cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }

        return false;
    }
}

