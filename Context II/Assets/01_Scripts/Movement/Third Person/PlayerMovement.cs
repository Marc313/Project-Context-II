using UnityEngine;

// Third Person Player Movement
public class PlayerMovement : Movement
{
    public float currentMoveSpeed;
    public bool isInteracting;

    public bool verticalEnabled = true;
    public bool horizontalEnabled = true;
    public bool rotationEnabled = true;

    private Rigidbody rb;
    private FollowPlayer playerCamera;

    private Vector3 moveDirection;
    private Quaternion targetRotation;


    private void Awake()
    {
        playerCamera = FindObjectOfType<FollowPlayer>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isInteracting)
        {
            HandleMoveInput();
            if (rotationEnabled) UpdateTargetRotation();
        }
    }

    private void HandleMoveInput()
    {
        float vertInput = verticalEnabled ? Input.GetAxisRaw("Vertical") : 0;
        float horInput = horizontalEnabled ? Input.GetAxisRaw("Horizontal") : 0;

        if (vertInput != 0 || horInput != 0)
        {
            Move(vertInput, horInput);
        }
    }

    private void Move(float vert, float hor)
    {
        moveDirection = (playerCamera.transform.forward * vert + playerCamera.transform.right * hor).normalized;
        Vector3 movement = moveDirection * currentMoveSpeed * Time.deltaTime;

        rb.position += movement;
    }

    private void UpdateTargetRotation()
    {
        targetRotation = GetFreeRotation();

        // Every frame, rotate towards the target rotation with a current speed.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private Quaternion GetFreeRotation()
    {
        return Quaternion.LookRotation(moveDirection);
    }
}
