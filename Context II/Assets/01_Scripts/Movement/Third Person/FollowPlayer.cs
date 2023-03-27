using MarcoHelpers;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float camSmoothing;
    public float sensitivityX;
    public Vector3 Offset;
    public Transform EnemyLockOn;

    public bool rotationEnabled = true;

    private float rotationY;
    private Transform Player;
    private PlayerMovement playerMovement;

    public Quaternion targetLookRotation { get; private set; }

    // Play is called before the first frame update
    void Awake()
    {
        Player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
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
        transform.position = Player.position + Offset;
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
            playerMovement = Player.GetComponent<PlayerMovement>();
        }
    }
}
