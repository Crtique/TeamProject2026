/*Script was written by CJ Robinson. handle player movement using ridgidbody*/
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    // --- Declare Variables ---
    public bool freeze { get; set; }
    public bool unlimited { get; set; }
    public bool restricted { get; set; }
    [Space]

    [Header("Player Movement")]
    public float moveSpeed  = 10f;

    private Vector3 move;

    [Header("Jumping Control")]
    public float jumpHeight    = 20f;
    public float jumpCooldown  = 5f;
    public float airMultiplier = 3;
    private bool ableToJump;

    [Header("Ground Drag")]
    public float gDrag = 5f; // Ground Drag

    [Header("Ground Checks")]
    public float playerHeight;
    public LayerMask isGround;
    private bool isGrounded;

    [Header("Slope Control")]
    const float slopeAngle = 75f;
    private RaycastHit slopeHit;
    private bool exitSlop;

    // --- Declare Components ---
    private Rigidbody rb;
    public Animator anim;

    // -- Animation bools --
    bool isRunning;
    bool isJumping;
    public bool isFalling {  get; set; }

    void Awake()
    {
        // Grab the Ridgidbody Component at the start of the game
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Animation();

        // Check if the player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGround);
        JumpReset();

        Inputs();

        // Handle the drag
        if (isGrounded)
            rb.linearDamping = gDrag;
        else
            rb.linearDamping = 0;

        // Freez Player Rotation
        if (freeze)
        {
            rb.linearVelocity = Vector3.zero;
        }
        else if (unlimited)
        {
            return;
        }
    }

    // FixedUpdate is called every fixed frame
    private void FixedUpdate()
    {
        PlayerMove();
        SpeedControl();
    }

    // -- Declare Animations Function --
    void Animation()
    {
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isFalling", isFalling);
    }

    // Player Input Function
    void Inputs()
    {
        // Call Jump funtions
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            ableToJump = false;
            isJumping = true;
            Jump();
        }
        else
        {
            isJumping = false;
        }

        // -- Animation Inputs --
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) && isGrounded)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    // Player Movement Function called every fixed frame
    void PlayerMove()
    {
        // When the player is on a ledge exit this function
        if (restricted) return;


        float horizontal = Input.GetAxisRaw("Horizontal");

        move = new Vector3(horizontal, 0f, 0f).normalized;

        // Move player Right
        if (horizontal > 0f)
            transform.eulerAngles = new Vector3(0f, 0f, 0f);

        // Move player Left
        else if (horizontal < 0f)
            transform.eulerAngles = new Vector3(0f, 180f, 0f);

        if (OnSlope() && !exitSlop)
        {
            rb.AddForce(10f * moveSpeed * GetSlopeDirection(move).normalized, ForceMode.Force);

            if (rb.linearVelocity.y < 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        if (isGrounded)
        {
            isFalling = false;
            Debug.DrawRay(transform.position, Vector3.down, Color.magenta);
            rb.AddForce(10f * moveSpeed * move.normalized, ForceMode.Force);
        }

        // in air
        else
        {
            isFalling = true;

            rb.AddForce(10f * airMultiplier * moveSpeed * move.normalized, ForceMode.Force);
        }
    }

    // Player Speed Control function called every frame
    void SpeedControl()
    {
        // Limit slope speed
        if (OnSlope() && !exitSlop)
        {
            if (rb.linearVelocity.magnitude > moveSpeed)
                rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
        }
        // Limit speed on ground or in the air
        else
        {
            Vector3 flatVel = new(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            // Limit the velocity if need be
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
            }
        }

    }

    // Jump Function called every frame
    void Jump()
    {
        exitSlop = true;

        // Reset the Y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }
    void JumpReset()
    {
        if (!ableToJump && isGrounded)
        {
            ableToJump = true;
            exitSlop = false;
        }
    }

    // Slope Control
    public bool OnSlope()
    {
        // Check if we are on the slope and store that posisiton
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            // Calculate how steep the slope is with Vector3.Angle function
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            // Check if we are angled or not
            return angle < slopeAngle && angle != 0f;

        }

        return false;
    }

    // Get the direction we move on the slope
    public Vector3 GetSlopeDirection(Vector3 move)
    {
        return Vector3.ProjectOnPlane(move, slopeHit.normal).normalized;
    }
}