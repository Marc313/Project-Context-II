using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10;
    public float jumpHeight = 2;
    public float groundedOffset = 0.2f;
    public float gravity = -9.81f;

    public bool isInteracting;

    [SerializeField] private LayerMask jumpLayers;
    private CharacterController controller;
    private float yVelocity;
    private bool isGrounded;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInteracting)
        {
            checkGrounded();
            movementInput();
            jumpInput();
        }
    }

    // Handles the target movement
    void movementInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        if (isGrounded && yVelocity < 0)
        {
            yVelocity = -2f;
        }

        // Movement in the y-axis
        yVelocity += gravity * Time.deltaTime;

        // Movement in the x-axis and z-axis
        Vector3 movement = transform.right * xInput + transform.forward * zInput;

        controller.Move(movement * moveSpeed * Time.deltaTime);     
        controller.Move(transform.up * yVelocity * Time.deltaTime);
    }

    // Handles the jump input of the target
    void jumpInput()
    {
        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    // Calculates if the target is grounded
    void checkGrounded()
    {
        float playerHeight = GetComponent<Collider>().bounds.size.y;    // Height of the target is equal to the height of its collider
        float playerBottomY = transform.position.y - playerHeight / 2;
        Vector3 playerBottom = new Vector3(transform.position.x, playerBottomY, transform.position.z);

        isGrounded = Physics.CheckSphere(playerBottom, groundedOffset, jumpLayers);
    }
}
