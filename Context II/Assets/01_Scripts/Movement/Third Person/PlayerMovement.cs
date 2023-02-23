using UnityEngine;

public class PlayerMovement : Movement
{
    public float currentMoveSpeed;

    private Rigidbody rb;
    private FollowPlayer playerCamera;

    private Vector3 moveDirection;
    private Quaternion targetRotation;

    public bool isInteracting;

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
            UpdateTargetRotation();
        }
    }

    private void HandleMoveInput()
    {
        float vertInput = Input.GetAxisRaw("Vertical");
        float horInput = Input.GetAxisRaw("Horizontal");

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
