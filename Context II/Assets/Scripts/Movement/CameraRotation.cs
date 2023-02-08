using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [Header("Camera Position")]
    public Vector3 offsetPlayer;

    [Header("Mouse Setting")]
    public float sensitivity;
    public float angleBoundaryY = 90f;

    [Header("References")]
    [SerializeField] private Transform target;

    private PlayerController playerController;
    private Transform camera;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   // Locks the cursor in the center of the screen

        camera = transform;
        transform.localPosition = offsetPlayer;
        transform.rotation = target.rotation;
    }

    void Update()
    {
        FindPlayerController();

        if (playerController == null || !playerController.isInteracting)
        {
            MouseXInput();
            MouseYInput();
        }
    }

    // Rotates the target when the mouse is moved in the x-direction
    private void MouseXInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        target.Rotate(Vector3.up * mouseX);
    }

    // Rotates the camera when the mouse is moved in the y-direction
    private void MouseYInput()
    {
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -angleBoundaryY, angleBoundaryY);

        camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void FindPlayerController()
    {
        if (playerController == null)
        {
            // Try to find the PlayerController script. This will only work if the target is a player
            playerController = target.GetComponent<PlayerController>();
        }
    }
}
