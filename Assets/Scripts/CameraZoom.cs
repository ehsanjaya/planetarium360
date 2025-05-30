using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class CameraZoom : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private Camera cameraLeft;
    [SerializeField] private Camera cameraRight;
    [SerializeField] private Camera cameraFull;
    [Header("Control Player")]
    public ControlPlayer controlPlayer = new ControlPlayer();
    [Header("Slider UI")]
    public GameObject sliderUI;
    public Slider slider;
    private float lastSliderValue;
    [Header("Audio Source Player")]
    public AudioSource audioSource;
    public AudioResource audioResource;
    [Header("Zoom")]
    public float zoom = 60f;
    public float zoomMutiplier = 4f;
    public float minZoom = 30f;
    public float maxZoom = 100f;
    public float smoothTime = 0.25f;
    [Header("Slider Controls Desktop")]
    public SliderControl zoomMultiplierSliderDesktop;
    [Header("Slider Controls Mobile")]
    public SliderControl zoomMultiplierSliderMobile;
    [Header("Slider Controls VR")]
    public SliderControl zoomMultiplierSliderVR;
    [Header("Key Controls VR")]
    public KeyBindingControl zoomInKeyBindVR;
    public KeyBindingControl zoomOutKeyBindVR;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraLeft.fieldOfView = zoom;
        cameraRight.fieldOfView = zoom;
        cameraFull.fieldOfView = zoom;

        slider.minValue = minZoom;
        slider.maxValue = maxZoom;
        slider.value = 60f;

        audioSource.loop = true;
        audioSource.volume = 0f;
        audioSource.Play();

        lastSliderValue = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        float velocity = 0f;

        if (controlPlayer.gameControlMode == "Desktop")
        {
            zoomMutiplier = zoomMultiplierSliderDesktop.value;

            sliderUI.SetActive(false);
            zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomMutiplier * Time.timeScale;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            cameraFull.fieldOfView = Mathf.SmoothDamp(cameraFull.fieldOfView, zoom, ref velocity, smoothTime);
        }
        else if (controlPlayer.gameControlMode == "Mobile")
        {
            zoomMutiplier = zoomMultiplierSliderMobile.value;

            sliderUI.SetActive(true);
            zoom = slider.value * Time.timeScale;
            cameraFull.fieldOfView = Mathf.SmoothDamp(cameraFull.fieldOfView, zoom, ref velocity, smoothTime);

            if (slider.value != lastSliderValue)
            {
                audioSource.resource = audioResource;
                audioSource.volume = 1f;
                lastSliderValue = slider.value;
            }
            else
            {
                audioSource.volume = 0f;
            }
        }
        else if (controlPlayer.gameControlMode == "VR")
        {
            Vector2 virtualKey = Vector2.zero;

            if (zoomInKeyBindVR.axis.magnitude > 0)
            {
                if (Input.GetAxisRaw("Horizontal") == zoomInKeyBindVR.axis.x && zoomInKeyBindVR.axis.x != 0) virtualKey.x = 1;
                else if (Input.GetAxisRaw("Vertical") == zoomInKeyBindVR.axis.y && zoomInKeyBindVR.axis.y != 0) virtualKey.x = 1;
            }
            else
            {
                virtualKey.x = Convert.ToInt32(Input.GetKey(zoomInKeyBindVR.keyCode));
            }

            if (zoomOutKeyBindVR.axis.magnitude > 0)
            {
                if (Input.GetAxisRaw("Horizontal") == zoomOutKeyBindVR.axis.x && zoomOutKeyBindVR.axis.x != 0) virtualKey.y = 1;
                else if (Input.GetAxisRaw("Vertical") == zoomOutKeyBindVR.axis.y && zoomOutKeyBindVR.axis.y != 0) virtualKey.y = 1;
            }
            else
            {
                virtualKey.y = Convert.ToInt32(Input.GetKey(zoomOutKeyBindVR.keyCode));
            }

            if (virtualKey.normalized.magnitude > 0)
            {
                if (zoom != minZoom && zoom != maxZoom)
                {
                    audioSource.resource = audioResource;
                    audioSource.volume = 1f;
                }
                else
                {
                    audioSource.volume = 0f;
                }
            }
            else
            {
                audioSource.volume = 0f;
            }

            zoomMutiplier = zoomMultiplierSliderVR.value;

            sliderUI.SetActive(false);
            zoom -= ((virtualKey.x - virtualKey.y) * 0.02f) * zoomMutiplier * Time.timeScale;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            cameraLeft.fieldOfView = Mathf.SmoothDamp(cameraLeft.fieldOfView, zoom, ref velocity, smoothTime);
            cameraRight.fieldOfView = Mathf.SmoothDamp(cameraRight.fieldOfView, zoom, ref velocity, smoothTime);
        }
    }

    public void OnResetCameraZoom(float defaultZoom)
    {
        zoom = defaultZoom;
    }
}
