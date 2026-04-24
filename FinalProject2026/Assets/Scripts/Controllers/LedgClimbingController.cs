using UnityEngine;

public class LedgClimbingController : MonoBehaviour
{
    // -- Components --
    private Rigidbody rb;
    private PlayerController player;

    [Header("Ledge Grabbing")]
    public float moveToLedgeSpeed;
    public float maxLedgeGrabDistance;

    public float minTimeOnLedge;
    private float currentTimeOnLedge;

    public bool isOnLedge;

    [Header("Ledge Jumping")]
    public float ledgeJumpUpwardForce;

    [Header("Exiting")]
    public bool exitLedge;
    public float exitLedgeTime;
    private float exitLedgeTimer;

    [Header("Ledge Checks")]
    public float ledgeCheckLength;
    public float ledgeSphereCastRadius;
    public LayerMask isLedge;

    private Transform lastLedge;
    private Transform currentLedge;

    private RaycastHit ledgeHit;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        LedgeDetection();
        StateMachine();
    }


    private void StateMachine()
    {
        // Holding onto the ledge
        if (isOnLedge)
        {
            FreezeRidgidbodyOnLedge();

            // increase the time while on the ledge
            currentTimeOnLedge += Time.deltaTime;


            // Exit the ledge
            if (currentTimeOnLedge > minTimeOnLedge && Input.GetKeyDown(KeyCode.Space)) // When our time on the ledge is higher than minimum time and we have pressed the Jump key
                ExitLedgeHold();

            // When the space key is pressed Jump off the ledge upwards
            if (Input.GetKeyDown(KeyCode.Space))
                LedgeJump();
        }

        // Exit Ledge
        else if (exitLedge)
        {
            // When the timer is greater than zero count it down
            if (exitLedgeTimer > 0)
                exitLedgeTimer -= Time.deltaTime;
            
            // if not set we havent exited the ledge
            else
                exitLedge = false;
        }

    }


    private void LedgeDetection()
    {
        // Detect the direction of what ledge we are facing
        Vector3 ledgeGrabDirection = transform.right;

        // Create a sphere to where the ledge is when the player is facing forwards
        bool ledgeDetect = Physics.SphereCast(transform.position, ledgeSphereCastRadius, ledgeGrabDirection, out ledgeHit, ledgeCheckLength, isLedge);
        
        // If you can't find the ledge then leave the function
        if (!ledgeDetect) return;

        // Check how far we are from the ledge
        float distanceToLedge = Vector3.Distance(transform.position, ledgeHit.transform.position);
        if (ledgeHit.transform == lastLedge) return;

        // Check if the distance is less than out maximum distance and if we are not on a ledge
        // if so enter the ledge holding function
        if (distanceToLedge < maxLedgeGrabDistance && !isOnLedge)
            EnterLedgeHold();
    }


    private void LedgeJump()
    {
        ExitLedgeHold();

        // Delay the jump force by 0.05 seconds
        Invoke(nameof(DeleyJumpForce), 0.05f);
    }
    private void DeleyJumpForce()
    {
        // Multiply the jumpforce to move the player up on the Y axis 
        Vector3 forceToAdd = Vector3.up * ledgeJumpUpwardForce;

        // Reset the players velocity
        rb.linearVelocity = Vector3.zero;

        // Add the Force so that the player jump upward while on a ledge
        rb.AddForce(forceToAdd, ForceMode.Impulse);
    }


    private void EnterLedgeHold()
    {
        // Set isOnLedge to True when we are ledge holding
        isOnLedge = true;

        player.unlimited = true;
        player.restricted = true;

        // get both the current and final transforms of the ledge placements
        currentLedge = ledgeHit.transform;
        lastLedge = ledgeHit.transform;

        // Disable the Gravity and remove all momentum
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
    }


    private void FreezeRidgidbodyOnLedge()
    {
        // keep gravity off
        rb.useGravity = false;

        // Calucalte direction to the ledge
        Vector3 directionToLedge = currentLedge.position - transform.position;
        float distanceToLedge = Vector3.Distance(transform.position, currentLedge.position);

        // Move player towards the ledge
        if (distanceToLedge > 1f)
        {
            if (rb.linearVelocity.magnitude < moveToLedgeSpeed)
                rb.AddForce(1000f * moveToLedgeSpeed * Time.deltaTime * directionToLedge.normalized);
        }

        // Hold onto ledge
        else
        {
            if (!player.freeze) player.freeze = true;
        }

        // Exit Ledge hold
        if (distanceToLedge > maxLedgeGrabDistance)
            ExitLedgeHold();
    }


    private void ExitLedgeHold()
    {
        // exit the ledge
        exitLedge = true;

        // Start the ledge timer
        exitLedgeTimer = exitLedgeTime;

        // no longer on the ledge
        isOnLedge = false;

        // reset the ledge time
        currentTimeOnLedge = 0f;

        // freeze, and restrict are disable
        player.restricted = false;
        player.freeze = false;

        // reanble the players gravity
        rb.useGravity = true;

        // set a delay of 1 seconed to rest the ledge
        StopAllCoroutines();
        Invoke(nameof(ResetLastLedge), 1f);
    }


    private void ResetLastLedge()
    {
        lastLedge = null;
    }
}
