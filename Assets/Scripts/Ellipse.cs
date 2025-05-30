using UnityEngine;

public class Ellipse : MonoBehaviour
{
    [Header("Point / Orbit")]
    public Transform point;
    [Header("Axial Tilt")]
    [Range(-360f, 360f)] public float axialTilt = 0;
    [Header("Ellipse")]
    public bool isRandom = false;
    [Range(-360f, 360f)] public float ellipseRotation = 0f;
    [Range(-360f, 360f)] public float direction = 0;
    public float radius = 0;
    public float speed = 0;
    public float angle = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isRandom == true)
        {
            direction = Random.Range(-360f, 360f);
        }

        angle = direction * 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        float angleRad = ellipseRotation * Mathf.Deg2Rad;
        float x = point.position.x + radius * Mathf.Cos(angle);
        float y = point.position.y + radius / 0.5f * Mathf.Sin(angle) * Mathf.Sin(angleRad);
        float z = point.position.z + radius / 0.5f * Mathf.Sin(angle) * Mathf.Cos(angleRad);
        
        transform.position = new Vector3(x, y, z);
        angle += speed * 0.000001f * Time.deltaTime;

        if (angle >= 360f)
        {
            angle = 0f;
        }
    }
}
