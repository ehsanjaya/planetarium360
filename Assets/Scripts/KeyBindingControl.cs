using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindingControl : MonoBehaviour
{
    [Header("Keycode")]
    public KeyCode defaultKeyCode;
    public KeyCode keyCode;
    [Header("Axis")]
    public Vector2 defaultAxis;
    public Vector2 axis;
    [Header("Text")]
    public Text keyText;
    public Text keyButtonText;
    [Header("Button")]
    public Button keyBindingButton;
    private bool isKeyBind = false;
    private bool isPressedKey = false;
    [Header("Control Player Script")]
    public ControlPlayer controlPlayer = new ControlPlayer();

    void Awake()
    {
        isKeyBind = false;
        isPressedKey = false;

        keyCode = defaultKeyCode;
        axis = defaultAxis;

        if (axis.magnitude > 0f)
        {
            if (axis.x > 0) keyText.text = "Virtual Left";
            else if (axis.x < 0) keyText.text = "Virtual Right";
            else if (axis.y > 0) keyText.text = "Virtual Up";
            else if (axis.y < 0) keyText.text = "Virtual Down";
        }
        else
        {
            keyText.text = keyCode.ToString();
        }

        keyButtonText.text = "Set Binding";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isKeyBind == true)
        {
            if (controlPlayer.gameControlMode == "VR")
            {
                for (int i = 0; i < 20; i++)
                {
                    KeyCode key = (KeyCode)((int)KeyCode.JoystickButton0 + i);
                    if (Input.GetKeyDown(key))
                    {
                        axis = Vector2.zero;
                        isPressedKey = true;
                        keyCode = key;
                    }
                }

                axis = new Vector2(
                    Input.GetAxisRaw("Horizontal") * Time.unscaledTime,
                    Input.GetAxisRaw("Vertical") * Time.unscaledTime
                ).normalized;

                if (axis.magnitude > 0f)
                {
                    isPressedKey = true;
                    keyCode = KeyCode.None;
                    Debug.Log(axis);
                }
            }
            else
            {
                foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(key))
                    {
                        axis = Vector2.zero;
                        isPressedKey = true;
                        keyCode = key;
                    }
                }
            }
        }
    }

    public void OnSetKeyBind()
    {
        if (isKeyBind == false)
        {
            isKeyBind = true;
            keyBindingButton.interactable = false;
            StartCoroutine(WaitForKeyPress());
        }
    }

    public void OnResetKeyBind()
    {
        isKeyBind = false;
        isPressedKey = false;

        keyCode = defaultKeyCode;
        keyButtonText.text = "Set Binding";
        axis = defaultAxis;

        if (axis.magnitude > 0f)
        {
            if (axis.x > 0) keyText.text = "Virtual Left";
            else if (axis.x < 0) keyText.text = "Virtual Right";
            else if (axis.y > 0) keyText.text = "Virtual Up";
            else if (axis.y < 0) keyText.text = "Virtual Down";
        }
        else
        {
            keyText.text = keyCode.ToString();
        }

        keyBindingButton.interactable = true;
    }

    IEnumerator WaitForKeyPress()
    {
        isPressedKey = false;
        keyButtonText.text = "Insert Key";

        yield return new WaitUntil(() => isPressedKey == true);

        if (axis.magnitude > 0f)
        {
            if (axis.x > 0) keyText.text = "Virtual Left";
            else if (axis.x < 0) keyText.text = "Virtual Right";
            else if (axis.y > 0) keyText.text = "Virtual Up";
            else if (axis.y < 0) keyText.text = "Virtual Down";
        }
        else
        {
            keyText.text = keyCode.ToString();
        }

        keyButtonText.text = "Set Binding";
        isPressedKey = false;
        isKeyBind = false;
        keyBindingButton.interactable = true;
    }
}
