using UnityEngine;

public class Orientation : MonoBehaviour
{
    [Header("Camera")]
    public Transform camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(camera.rotation.eulerAngles.x, camera.rotation.eulerAngles.y, camera.rotation.eulerAngles.z);
    }
}
