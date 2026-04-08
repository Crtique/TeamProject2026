/*Script was written by CJ Robinson. handle player movement using ridgidbody*/
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // --- Declare Variables ---
    [Header("Player Movement")]
    public float speed = 10f;

    public float jumpHeight = 20f;
    public float jumpCooldown = 5f;
    public float airMultiplier = 3;
    private bool ableToJump;

    [Header("Ground and Air Drag")]
    public float gDrag = 5f; // Ground Drag
    public float aDrag = 3f; // Air Drag

    [Header("Ground Checks")]
    public float playerHeight;
    public LayerMask isGround;
    private bool isGrounded;

    [Header("Slideing Checks")]
    public float height;
    public float slideHeight;
    private bool ableToSlide;

    // --- Declare Components ---
    private Rigidbody rb;

    void Awake()
    {
        // Grab the Ridgidbody Component at the start of the game
        rb = GetComponent<Rigidbody>();

        height = transform.localScale.y;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGround);

        Inputs();
        SpeedControl();

        // Handle the drag
        if (isGrounded)
            rb.linearDamping = gDrag;
        else
            rb.linearDamping = aDrag;
    }

    // FixedUpdate is called every fixed frame
    private void FixedUpdate()
    {
        PlayerMove();
    }

    void Inputs()
    {
        // Call Jump funtions
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            ableToJump = false;
            Jump();

            Invoke(nameof(JumpReset), jumpCooldown);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ableToSlide = false;
            rb.AddForce(Vector3.right * 100f, ForceMode.Acceleration);
            Slide();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            ableToSlide = true;
            transform.localScale = new Vector3(transform.localScale.x, height, transform.localScale.z);
        }

    }

    // Player Movement Function called every fixed frame
    void PlayerMove()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (isGrounded)
                rb.AddForce(-Vector3.right * speed * 10f, ForceMode.Force);
            else if (!isGrounded)
                rb.AddForce(-Vector3.right * speed * 10f * airMultiplier, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (isGrounded)
                rb.AddForce(Vector3.right * speed * 10f, ForceMode.Force);
            else if (!isGrounded)
                rb.AddForce(Vector3.right * speed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    // Player Speed Control function called every frame
    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        
        // Limit the velocity if need be
        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }

    }

    // Jump Function called every frame
    void Jump()
    {
        // Reset the Y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }
    void JumpReset()
    {
        ableToJump = true;
    }

    void Slide()
    {
        transform.localScale = new Vector3(transform.localScale.x, slideHeight, transform.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    }
}