using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class FPSController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera fpsCamera;

    [Header("Movement Parameters")]
    [Tooltip("Movement speed at full speed in metres per second.")]
    [SerializeField] private float moveSpeed = 1;
    [Tooltip("Rate of acceleration towards the move speed, in metres per second squared.")]
    [SerializeField] private float accelerationRate = 1;
    [Tooltip("Rate of deceleration towards zero when no inputs, in metres per second squared.")]
    [SerializeField] private float decelerationRate = 1;
    [Tooltip("Rate of natural deceleration simulating air resistance - useful to slow player mid-air.")]
    [SerializeField] private float dragCoefficient = 0.02f;
    [Tooltip("Distance in which ground is detected to snap the player down. Useful for slopes and steps.")]
    [SerializeField] private float groundSnapDistance = 0.5f;
    [Tooltip("Y velocity at the beginning of a jump.")]
    [SerializeField] private float jumpForce = 5;
    [Tooltip("Enables movement inputs for in-air control.")]
    [SerializeField] private bool canMoveInAir = false;

    [Header("Look Parameters")]
    [SerializeField] private float lookSensitivity = 0.4f;
    [SerializeField] private float maxVerticalAngle = 80;

    // Component references
    private Rigidbody rb;
    private CapsuleCollider cc;

    // Movement live data
    private Vector3 movementVelocity;
    private float yVelocity;
    private Vector2 lookRotation;
    private bool isGrounded;
    private float groundDistance;
    private Vector3 groundNormal;
    private bool isJumping;
    private bool isFalling;

    // Input buffers
    private Vector2 inMovement;
    private bool inJump;


    #region Unity Messages
    private void Awake()
    {
        cc = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        SetRigidbodyConstrains();

        // Hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        CheckGround();
        if (isGrounded || groundDistance < groundSnapDistance || canMoveInAir)
            HandleMovement();
        HandleGravity();
        UpdateRigidbodyData();
        HandleDrag();
    }
    #endregion

    #region Input Messages
    private void OnMove(InputValue movementValue)
    {
        if (!enabled) return;
        inMovement = movementValue.Get<Vector2>();
    }

    private void OnLook(InputValue lookValue)
    {
        if (!enabled) return;
        Vector2 lookDelta = lookValue.Get<Vector2>();
        lookDelta.y = -lookDelta.y; // Invert up and down
        lookRotation += lookDelta * lookSensitivity;
        lookRotation.y = Mathf.Clamp(lookRotation.y, -maxVerticalAngle, maxVerticalAngle);
        rb.MoveRotation(Quaternion.AngleAxis(lookRotation.x, Vector3.up));
        fpsCamera.transform.localRotation =
            Quaternion.AngleAxis(lookRotation.y, Vector3.right);
    }

    private void OnJump()
    {
        if (!enabled) return;
        inJump = true;
    }
    #endregion

    private void HandleMovement()
    {
        // Decelerate if no input
        if (inMovement == Vector2.zero)
        {
            movementVelocity = Vector3.MoveTowards(movementVelocity, Vector3.zero,
                decelerationRate * Time.fixedDeltaTime);
        }
        // Slope movement
        else if (groundNormal != Vector3.up)
        {
            Vector3 moveDirection = rb.rotation * new Vector3(inMovement.x, 0, inMovement.y);
            Vector3 slopedDirection = Vector3.ProjectOnPlane(moveDirection, groundNormal);
            movementVelocity = Vector3.MoveTowards(movementVelocity, slopedDirection * moveSpeed,
                accelerationRate * Time.fixedDeltaTime);
        }
        // Acceleration from move input
        else
        {
            Vector3 moveDir = rb.rotation * new Vector3(inMovement.x, 0, inMovement.y);
            movementVelocity = Vector3.MoveTowards(movementVelocity,
                moveDir * moveSpeed, accelerationRate * Time.fixedDeltaTime);
            movementVelocity = Vector3.ClampMagnitude(movementVelocity, moveSpeed);
        }
    }

    private void HandleDrag()
    {
        movementVelocity *= 1 - dragCoefficient * Time.fixedDeltaTime;
    }

    private void HandleGravity()
    {
        if (isGrounded)
        {
            if (inJump && !isJumping)
            {
                yVelocity = jumpForce;
                movementVelocity.y = 0;
                isJumping = true;
                isGrounded = false;
            }
            else
            {
                yVelocity = 0;
            }
        }
        else if (groundDistance < groundSnapDistance && !isJumping && !isFalling)
        {
            rb.Move(rb.position - new Vector3(0, groundDistance, 0), rb.rotation);
        }


        if (isJumping)
        {
            isJumping = !isGrounded;
            movementVelocity.y = 0; // Stops slops affecting jump height
        }

        yVelocity += Physics.gravity.y * Time.fixedDeltaTime;

        // Reset jump input
        inJump = false;
    }

    private void UpdateRigidbodyData()
    {
        rb.linearVelocity = new Vector3(
            movementVelocity.x, movementVelocity.y + yVelocity, movementVelocity.z);
    }

    private void SetRigidbodyConstrains(bool on = true)
    {
        rb.constraints = on ? RigidbodyConstraints.FreezeRotation : RigidbodyConstraints.None;
        rb.useGravity = !on;
    }

    private void CheckGround()
    {
        float castRadius = cc.radius;
        Vector3 castStart = new Vector3(
            cc.bounds.center.x,
            cc.bounds.min.y + castRadius + 0.05f,
            cc.bounds.center.z);

        Ray ray = new Ray(castStart, Physics.gravity);
        RaycastHit hit;
        if (Physics.SphereCast(ray, castRadius, out hit, 0.06f, int.MaxValue,
            QueryTriggerInteraction.Ignore))
        {
            isGrounded = true;
            isFalling = false;
            groundNormal = hit.normal;
        }
        else
        {
            isGrounded = false;
            groundNormal = Vector3.down;
        }

        // For additional accuracy on uneven ground, check a further distance with a raycast
        Vector3 rayStart = new Vector3(
            cc.bounds.center.x,
            cc.bounds.min.y,
            cc.bounds.center.z);
        if (Physics.Raycast(rayStart, Physics.gravity, out hit,
            groundSnapDistance, int.MaxValue, QueryTriggerInteraction.Ignore))
        {
            groundDistance = hit.distance;
            groundNormal = hit.normal;
        }
        else
        {
            isFalling = true;
            groundDistance = float.PositiveInfinity;
        }
    }
}
