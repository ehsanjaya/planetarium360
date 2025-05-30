using UnityEngine;

public class ControlShader : MonoBehaviour
{
    public Transform point;
    public Transform transformPosition;
    private Material material;
    private Camera camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main;
        material = GetComponent<MeshRenderer>().sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Quaternion.LookRotation(transformPosition.position - point.position);
        Vector3 forwardDirection = rotation * Vector3.forward;

        material.SetVector("_Direction", forwardDirection);
    }
}
