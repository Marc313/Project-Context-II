using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [Header("Camera Position")]
    public Vector3 offsetPlayer;

    [Header("Mouse Setting")]
    public float sensitivity;
    public float angleBoundaryY = 90f;

    [Header("References")]
    [SerializeField] private Transform player;

    private Transform camera;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   // Locks the cursor in the center of the screen

        camera = transform;
        transform.localPosition = offsetPlayer;
        transform.rotation = player.rotation;
    }

    void Update()
    {
        mouseXInput();
        mouseYInput();
    }

    // Rotates the player when the mouse is moved in the x-direction
    void mouseXInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        player.Rotate(Vector3.up * mouseX);
    }

    // Rotates the camera when the mouse is moved in the y-direction
    void mouseYInput()
    {
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -angleBoundaryY, angleBoundaryY);

        camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
