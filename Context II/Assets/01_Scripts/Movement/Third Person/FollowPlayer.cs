using MarcoHelpers;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private float camSmoothing;
    [SerializeField] private float sensitivityX;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform player;

    public bool rotationEnabled = true;

    private float rotationY;
    private PlayerMovement playerMovement;

    public Quaternion targetLookRotation { get; private set; }

    private void Start()
    {
        FindPlayerController();
        playerMovement?.InsertCamera(this);
    }

    void Update()
    {
        FindPlayerController();

        if (playerMovement == null || !playerMovement.isInteracting)
        {
            MoveToPlayer();
            if (rotationEnabled) RotateCameraAlongMouse();
        }
    }

    private void MoveToPlayer()
    {
        // Move the camera along with the player
        transform.position = player.position + offset;
    }

    public void RotateCameraAlongMouse()
    {
        // Input on the mouse X axis
        float mouseX = Input.GetAxis("Mouse X");
        rotationY += mouseX * sensitivityX * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(transform.rotation.x, rotationY, transform.rotation.z);
    }

    private void FindPlayerController()
    {
        if (playerMovement == null)
        {
            // Try to find the PlayerController script. This will only work if the target is a player
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }
}
