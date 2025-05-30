using UnityEngine;

public class Rotation : MonoBehaviour
{
    [Header("Rotation")]
    public Vector3 rotation = new Vector3();
    [Header("Axial Tilt")]
    [Range(0f, 360f)] public float axialTilt = 0f;
    [Header("Speed Rotation")]
    public float speed = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.rotation = Quaternion.Euler(axialTilt + rotation.x, rotation.y, rotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, speed * 0.001f * Time.timeScale, Space.Self);
    }
}
