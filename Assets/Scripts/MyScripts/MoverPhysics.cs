using System;
using UnityEngine;
public class MoverPhysics : MonoBehaviour
{
    [SerializeField] float Player_Velocity = 100.0f;
    [SerializeField] float Player_Move_Acceleration = 100.0f;
    [SerializeField] float Player_Jump_Acceleration = 100.0f;
    [SerializeField] float groundCheckDistance = 0.5f;
    [SerializeField] float hitDamping = 0.5f;
    [SerializeField] float hitControlThreshold = 0.1f;
    [SerializeField] float airControl = 0.0f;
    [SerializeField] float sphereRadius = 0.5f;
    [SerializeField] bool DebugMode = false;
    [SerializeField] float power = 0.0f;
    [SerializeField] float slopeLimit = 45.0f;

    float Velocity = 0.0f;
    float root2 = Mathf.Sqrt(2);
    float AccelerationMultiplier = 1.0f;
    float constant = 0;
    float NormalAngle = 0;

    Vector3 InputVector = Vector3.zero;
    Vector3 Acceleration = Vector3.zero;
    Vector3 LinVelo = Vector3.zero;
    Vector3 Drag_Acc = Vector3.zero;

    private bool isGrounded;
    bool hit = false;
    bool Jump = false;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        LinVelo = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        Move();
        JumpF();
    }

    private void JumpF()
    {
        if (Jump)
        {
            rb.AddForce(Vector3.up * Player_Jump_Acceleration * rb.mass, ForceMode.Impulse);
            Jump = false;
        }
    }

    private void Move()
    {
        Acceleration = Player_Move_Acceleration * InputVector;
        Drag_Acc = LinVelo;
        constant = Player_Move_Acceleration / Player_Velocity;
        rb.AddForce((Acceleration - constant * Drag_Acc) * AccelerationMultiplier, ForceMode.Acceleration);
    }

    void Update()
    {
        GroundCheck();
        PlayerInput();
        ControllerForcesHandler();
        DebugModeFunc();
    }

    private void DebugModeFunc()
    {
        Velocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;
        if (DebugMode)
        {
            Debug.Log("<color=red>Velocity : " + Velocity + " m/s</color>" +
                  "  <color=green>Is Grounded : " + isGrounded + "</color>" +
                  "  <color=yellow>Hit : " + hit + "</color>" +
                  "  <color=orange>Constant : " + constant + "</color>" +
                  "  <color=purple>Input Vector : " + InputVector + "</color>");
        }
    }

    public void GroundCheck()
    {
        RaycastHit check;
        bool temp = Physics.SphereCast(transform.position, sphereRadius, Vector3.down, out check, groundCheckDistance);
        NormalAngle = Vector3.Angle(check.normal, Vector3.up);
        if (NormalAngle <= slopeLimit && temp)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void ControllerForcesHandler()
    {
        if (isGrounded)
        {
            if (!hit)
            {
                rb.linearDamping = 0.0f;
                AccelerationMultiplier = 1;
            }
            else
            {
                AccelerationMultiplier = 0;
                rb.linearDamping = hitDamping;
                if (Velocity <= Mathf.Sqrt(3 * hitControlThreshold * hitControlThreshold))
                {
                    hit = false;
                }
            }
        }
        else
        {
            if (!hit)
            {
                rb.linearDamping = 0.0f;
                AccelerationMultiplier = airControl;
            }
            else
            {
                AccelerationMultiplier = 0;
                rb.linearDamping = 0.0f;
            }
        }
    }

    private void PlayerInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(horizontalInput) > 0.0f && Mathf.Abs(verticalInput) > 0.0f)
        {
            InputVector = new Vector3(horizontalInput / root2, 0, verticalInput / root2);
        }
        else if (Mathf.Abs(horizontalInput) > 0.0f ^ Mathf.Abs(verticalInput) > 0.0f)
        {
            InputVector = new Vector3(horizontalInput, 0, verticalInput);
        }
        else
        {
            InputVector = Vector3.zero;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hitter")
            hit = true;
    }
}
