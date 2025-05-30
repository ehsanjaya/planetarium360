using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ControlPlayer : MonoBehaviour
{
    [Header("Joystick Move")]
    [SerializeField] private FixedJoystick joystickMove;
    [Header("Fixed Joysticks")]
    [SerializeField] private GameObject fixedJoystickMove;
    [SerializeField] private GameObject fixedJoystickRotate;
    [Header("Transform Orientation")]
    public Transform transformOrientation;
    private Vector3 velocity = Vector3.zero;
    [Header("Game Control Mode")]
    public string gameControlMode = "None";
    [Header("Axis")]
    private float horizontal = 0f;
    private float vertical = 0f;
    [Header("Speed Movement")]
    public float acceleration = 10f;
    public float speed = 100f;
    [Header("Mouse Visible")]
    public bool mouseVisible = false;
    [Header("Key Controls Desktop")]
    public KeyBindingControl resetKeyBindDesktop;
    public KeyBindingControl mouseVisibleKeyBindDesktop;
    public KeyBindingControl forwardKeyBindDesktop;
    public KeyBindingControl leftKeyBindDesktop;
    public KeyBindingControl backwardKeyBindDesktop;
    public KeyBindingControl rightkeyBindDesktop;
    [Header("Key Controls VR")]
    public KeyBindingControl resetKeyBindVR;
    public KeyBindingControl forwardKeyBindVR;
    public KeyBindingControl leftKeyBindVR;
    public KeyBindingControl backwardKeyBindVR;
    public KeyBindingControl rightKeyBindVR;
    [Header("Input Fields Desktop")]
    public InputFieldControl resetPositionXInputFieldDesktop;
    public InputFieldControl resetPositionYInputFieldDesktop;
    public InputFieldControl resetPositionZInputFieldDesktop;
    [Header("Input Fields VR")]
    public InputFieldControl resetPositionXInputFieldVR;
    public InputFieldControl resetPositionYInputFieldVR;
    public InputFieldControl resetPositionZInputFieldVR;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        horizontal = 0f;
        vertical = 0f;

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            gameControlMode = "Desktop";
        }
        else
        {
            gameControlMode = "Mobile";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameControlMode == "Desktop")
        {
            horizontal = (Convert.ToInt32(Input.GetKey(rightkeyBindDesktop.keyCode) || Input.GetKey(KeyCode.RightArrow)) - Convert.ToInt32(Input.GetKey(leftKeyBindDesktop.keyCode) || Input.GetKey(KeyCode.LeftArrow))) * speed * Time.timeScale;
            vertical = (Convert.ToInt32(Input.GetKey(forwardKeyBindDesktop.keyCode) || Input.GetKey(KeyCode.UpArrow)) - Convert.ToInt32(Input.GetKey(backwardKeyBindDesktop.keyCode) || Input.GetKey(KeyCode.DownArrow))) * speed * Time.timeScale;

            if (Input.GetKey(resetKeyBindDesktop.keyCode))
            {
                transform.position = new Vector3(
                    float.Parse(resetPositionXInputFieldDesktop.value),
                    float.Parse(resetPositionYInputFieldDesktop.value),
                    float.Parse(resetPositionZInputFieldDesktop.value)
                );
            }
        }
        else if (gameControlMode == "Mobile")
        {
            float absHorizontal = Mathf.Abs(joystickMove.Horizontal);
            float absVertical = Mathf.Abs(joystickMove.Vertical);

            float realHorizontal = absHorizontal > absVertical ? joystickMove.Horizontal : 0;
            float realVertical = absHorizontal <= absVertical ? joystickMove.Vertical : 0;

            horizontal = realHorizontal * speed * Time.timeScale;
            vertical = realVertical * speed * Time.timeScale;
        }
        else if (gameControlMode == "VR")
        {
            Vector2 horizontalKey = Vector2.zero;
            Vector2 verticalKey = Vector2.zero;
            bool resetKey = false;

            if (leftKeyBindVR.axis.magnitude > 0)
            {
                if (Input.GetAxisRaw("Horizontal") == leftKeyBindVR.axis.x && leftKeyBindVR.axis.x != 0) horizontalKey.x = 1;
                else if (Input.GetAxisRaw("Vertical") == leftKeyBindVR.axis.y && leftKeyBindVR.axis.y != 0) horizontalKey.x = 1;
            }
            else
            {
                horizontalKey.y = Convert.ToInt32(Input.GetKey(leftKeyBindVR.keyCode));
            }

            if (rightKeyBindVR.axis.magnitude > 0)
            {
                if (Input.GetAxisRaw("Horizontal") == rightKeyBindVR.axis.x && rightKeyBindVR.axis.x != 0) horizontalKey.y = 1;
                else if (Input.GetAxisRaw("Vertical") == rightKeyBindVR.axis.y && rightKeyBindVR.axis.y != 0) horizontalKey.y = 1;
            }
            else
            {
                horizontalKey.x = Convert.ToInt32(Input.GetKey(rightKeyBindVR.keyCode));
            }

            if (forwardKeyBindVR.axis.magnitude > 0)
            {
                if (Input.GetAxisRaw("Horizontal") == forwardKeyBindVR.axis.x && forwardKeyBindVR.axis.x != 0) verticalKey.x = 1;
                else if (Input.GetAxisRaw("Vertical") == forwardKeyBindVR.axis.y && forwardKeyBindVR.axis.y != 0) verticalKey.x = 1;
            }
            else
            {
                verticalKey.x = Convert.ToInt32(Input.GetKey(forwardKeyBindVR.keyCode));
            }

            if (backwardKeyBindVR.axis.magnitude > 0)
            {
                if (Input.GetAxisRaw("Horizontal") == backwardKeyBindVR.axis.x && backwardKeyBindVR.axis.x != 0) verticalKey.y = 1;
                else if (Input.GetAxisRaw("Vertical") == backwardKeyBindVR.axis.y && backwardKeyBindVR.axis.y != 0) verticalKey.y = 1;
            }
            else
            {
                verticalKey.y = Convert.ToInt32(Input.GetKey(backwardKeyBindVR.keyCode));
            }

            horizontal = (horizontalKey.x - horizontalKey.y) * speed * Time.timeScale;
            vertical = (verticalKey.x - verticalKey.y) * speed * Time.timeScale;

            if (resetKeyBindVR.axis.magnitude > 0)
            {
                if (Input.GetAxisRaw("Horizontal") == resetKeyBindVR.axis.x && resetKeyBindVR.axis.x != 0) resetKey = true;
                else if (Input.GetAxisRaw("Vertical") == resetKeyBindVR.axis.y && resetKeyBindVR.axis.y != 0) resetKey = true;
            }
            else
            {
                resetKey = Input.GetKey(resetKeyBindVR.keyCode);
            }

            if (resetKey == true)
            {
                transform.position = new Vector3(
                    float.Parse(resetPositionXInputFieldVR.value),
                    float.Parse(resetPositionYInputFieldVR.value),
                    float.Parse(resetPositionZInputFieldVR.value)
                );

                resetKey = false;
            }
        }

        if (gameControlMode == "Mobile")
        {
            fixedJoystickMove.SetActive(true);
            fixedJoystickRotate.SetActive(true);
        }
        else
        {
            fixedJoystickMove.SetActive(false);
            fixedJoystickRotate.SetActive(false);
        }

        if (Input.GetKeyDown(mouseVisibleKeyBindDesktop.keyCode)) mouseVisible = !mouseVisible;
        
        if (mouseVisible == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        Vector3 targetVelocity = transformOrientation.forward * vertical + transformOrientation.right * horizontal;
        velocity = Vector3.Lerp(velocity, targetVelocity, acceleration * Time.deltaTime);

        transform.position += velocity * Time.deltaTime;
    }

    public void SetGameControlMode(string name)
    {
        gameControlMode = name;
    }
}
