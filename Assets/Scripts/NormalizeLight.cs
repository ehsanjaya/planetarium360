using UnityEngine;

public class NormalizeLight : MonoBehaviour
{
    public Light pointLight;
    public float baseIntensity = 1f;
    public float referenceDPI = 160f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float scale = Screen.dpi > 0 ? Screen.dpi / referenceDPI : 1f;
        pointLight.intensity = baseIntensity / scale;
    }
}
