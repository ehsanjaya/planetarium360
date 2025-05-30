using UnityEngine;
using UnityEngine.UI;

public class InputFieldControl : MonoBehaviour
{
    [Header("Value")]
    public string defaultValue = "";
    public string value = "";
    [Header("Input Field")]
    public InputField inputField;

    void Awake()
    {
        value = defaultValue;
        inputField.text = value;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSetInputField()
    {
        value = inputField.text;
    }

    public void OnResetInputField()
    {
        value = defaultValue;
        inputField.text = value;
    }
}
