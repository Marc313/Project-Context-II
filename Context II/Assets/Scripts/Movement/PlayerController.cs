using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    public float groundedOffset = 0.2f;
    public float gravity = -9.81f;

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
        checkGrounded();
        movementInput();
        jumpInput();
    }

    // Handles the player movement
    void movementInput()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

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

    // Handles the jump input of the player
    void jumpInput()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    // Calculates if the player is grounded
    void checkGrounded()
    {
        float playerHeight = GetComponent<Collider>().bounds.size.y;    // Height of the player is equal to the height of its collider
        float playerBottomY = transform.position.y - playerHeight / 2;
        Vector3 playerBottom = new Vector3(transform.position.x, playerBottomY, transform.position.z);

        isGrounded = Physics.CheckSphere(playerBottom, groundedOffset, jumpLayers);
    }
}
