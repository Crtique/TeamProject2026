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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LedgeDetection();
        StateMachine();
    }

    private void StateMachine()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        bool anyInputKeyPressed = horizontal != 0;

        // Holding onto the ledge
        if (isOnLedge)
        {
            FreezeRidgidbodyOnLedge();

            currentTimeOnLedge += Time.deltaTime;

            if (currentTimeOnLedge > minTimeOnLedge && anyInputKeyPressed)
                ExitLedgeHold();
        }
    }

    private void LedgeDetection()
    {
        // Create a sphere to where the ledge is when the player is facing forwards
        bool ledgeDetect = Physics.SphereCast(transform.position, ledgeSphereCastRadius, Vector3.right, out ledgeHit, ledgeCheckLength, isLedge);
        
        // If you can't find the ledge then leave the function
        if (!ledgeDetect) return;

        // Check how far we are from the ledge
        float distanceToLedge = Vector3.Distance(transform.position, ledgeHit.transform.position);
        if (ledgeHit.transform == lastLedge) return;

        if (distanceToLedge < maxLedgeGrabDistance && !isOnLedge)
            EnterLedgeHold();
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
                rb.AddForce(directionToLedge.normalized * moveToLedgeSpeed * 1000f * Time.deltaTime);
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
        isOnLedge = false;
        currentTimeOnLedge = 0f;

        player.restricted = false;
        player.freeze = false;

        rb.useGravity = true;

        StopAllCoroutines();
        Invoke(nameof(ResetLastLedge), 1f);
    }

    private void ResetLastLedge()
    {
        lastLedge = null;
    }
}
