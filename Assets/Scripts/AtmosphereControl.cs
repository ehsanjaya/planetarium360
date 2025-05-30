using UnityEngine;

public class AtmosphereControl : MonoBehaviour
{
    [Header("Point")]
    public Transform point;
    [Header("Target")]
    public Transform target;
    private Material material;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = GetComponent<MeshRenderer>().sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (target.position - point.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        material.SetVector("_Direction", rotation * Vector3.forward);
    }
}
