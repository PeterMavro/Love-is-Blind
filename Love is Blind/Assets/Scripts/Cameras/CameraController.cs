using UnityEngine;

// Makes the camera follow the player
public class CameraController : MonoBehaviour
{
    public Transform target;    
    public Vector3 offset;          
    public float zoomSpeed = 4f;    // How quickly we zoom
    public float minZoom = 5f;      
    public float maxZoom = 15f;     
    public float pitch = 2f;        // Pitch up the camera to look at head
    public float yawSpeed = 100f;   // How quickly we rotate

    private float _currentZoom = 10f;
    private float _currentYaw = 0f;
    private Transform _catchedTransform;

    private void Awake()
    {
        _catchedTransform = transform;
    }

    private void Update()
    {
        _currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        _currentZoom = Mathf.Clamp(_currentZoom, minZoom, maxZoom);

        // Adjust our camera's rotation around the player
        //currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        // Set our cameras position based on offset and zoom
        _catchedTransform.position = target.position - offset * _currentZoom;
        // Look at the player's head
        _catchedTransform.LookAt(target.position + Vector3.up * pitch);

        // Rotate around the player
        //_catchedTransform.RotateAround(target.position, Vector3.up, _currentYaw);
    }
}
