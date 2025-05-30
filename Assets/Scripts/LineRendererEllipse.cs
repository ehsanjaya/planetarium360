using UnityEditor;
using UnityEngine;

public class LineRendererEllipse : MonoBehaviour
{
    [Header("Point / Orbit")]
    public Transform point;
    LineRenderer lr;
    [Header("Ellipse")]
    [Range(-360f, 360f)] public float ellipseRotation = 0f;
    private Vector3[] positions = new Vector3[361];
    public float radius = 0f;
    public float angle = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float angleRad = ellipseRotation * Mathf.Deg2Rad;
        lr.positionCount = 361;
        angle = 0f;
        for (int i = 0; i < 361; i++)
        {
            float x = point.position.x + radius * Mathf.Cos(angle);
            float y = point.position.y + radius / 0.5f * Mathf.Sin(angle) * Mathf.Sin(angleRad);
            float z = point.position.z + radius / 0.5f * Mathf.Sin(angle) * Mathf.Cos(angleRad);
            positions[i] = new Vector3(x, y, z);
            angle += 2 * Mathf.PI / 360f;
        }
        lr.SetPositions(positions);
    }
}