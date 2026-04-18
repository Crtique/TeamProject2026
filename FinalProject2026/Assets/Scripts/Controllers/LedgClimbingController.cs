using UnityEngine;

public class LedgClimbingController : MonoBehaviour
{
    // -- Components --
    private Rigidbody rb;
    private PlayerController player;

    [Header("Ledge Checks")]
    public float ledgeCheckLength;
    public float ledgeSphereCastRadius;
    public LayerMask isLedge;

    private Transform lastLedge;
    private float currentLedge;

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
    }

    private void LedgeDetection()
    {
        // Create a sphere to where the ledge is when the player is facing forwards
        bool ledgeDetect = Physics.SphereCast(transform.position, ledgeSphereCastRadius, Vector3.forward, out ledgeHit, ledgeCheckLength, isLedge);
        
        // If you can't find the ledge then leave the function
        if (!ledgeDetect) return;

        // Check how far we are from the ledge
        float distanceToLedge = Vector3.Distance(transform.position, ledgeHit.transform.position);
    }

    private void EnterLedgeHold()
    {

    }

    private void FreezeRidgidbodyOnLedge()
    {

    }

    private void ExitLedgeHold()
    {

    }
}
