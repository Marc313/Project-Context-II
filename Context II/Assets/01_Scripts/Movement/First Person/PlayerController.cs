using MarcoHelpers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10;
    public float jumpHeight = 2;
    public float groundedOffset = 0.2f;
    public float gravity = -9.81f;
    [SerializeField] private LayerMask jumpLayers;

/*    [Header("Interacting")]
    public float interactRange;*/

    [HideInInspector] public bool isInteracting;

    private CharacterController controller;
    private float yVelocity;
    private bool isGrounded;
    private IInteractable closestInteractable;

    public void OnEnable()
    {
        EventSystem.Subscribe(EventName.MENU_OPENED, DisableSelf);
        EventSystem.Subscribe(EventName.MENU_CLOSED, EnableSelf);
    }

    public void OnDisable()
    {
        EventSystem.Unsubscribe(EventName.MENU_OPENED, DisableSelf);
        EventSystem.Unsubscribe(EventName.MENU_CLOSED, EnableSelf);
    }

    private void EnableSelf(object _value)
    {
        isInteracting = false;
    }

    private void DisableSelf(object _value)
    {
        isInteracting = true;
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInteracting)
        {
            CheckGrounded();
            MovementInput();
            JumpInput();
        }
    }

    // Handles the target movement
    private void MovementInput()
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
    private void JumpInput()
    {
        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    // Calculates if the target is grounded
    private void CheckGrounded()
    {
        float playerHeight = GetComponent<Collider>().bounds.size.y;    // Height of the target is equal to the height of its collider
        float playerBottomY = transform.position.y - playerHeight / 2;
        Vector3 playerBottom = new Vector3(transform.position.x, playerBottomY, transform.position.z);

        isGrounded = Physics.CheckSphere(playerBottom, groundedOffset, jumpLayers);
    }
}
