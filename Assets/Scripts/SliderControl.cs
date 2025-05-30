using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    [Header("Value")]
    public float defaultValue = 0f;
    public float value = 0f;
    [Header("Slider")]
    public Slider slider;
    [Header("Text")]
    public Text valueText;
    [Header("Slider Value")]
    public float minValue = 0f;
    public float maxValue = 1f;

    void Awake()
    {
        slider.minValue = minValue;
        slider.maxValue = maxValue;

        value = defaultValue;
        slider.value = value;
        valueText.text = value.ToString();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        value = slider.value;
        valueText.text = value.ToString();
    }

    public void OnResetScrollValue()
    {
        value = defaultValue;
        slider.value = value;
        valueText.text = value.ToString();
    }
}
