using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    [Header("Offset")]
    public Vector3 offset;
    [Header("Text")]
    public TMP_Text textMeshPro;
    public string text = "";
    Transform mainCamera;
    Transform unit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = GameObject.Find("Cameras").GetComponent<Transform>();
        unit = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        transform.position = unit.position + offset;
        textMeshPro.text = text;
    }
}
