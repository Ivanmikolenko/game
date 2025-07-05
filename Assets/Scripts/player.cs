using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] public float jumpForce = 5f;
    [SerializeField] public float startSpeed = 500f;
    [SerializeField] public float speed = 200f;
    [SerializeField] public float maxSpeed = 2000f;
    [SerializeField] public float rotationSpeed = 200f;
    [SerializeField] private Rigidbody2D bikeRB;
    [SerializeField] private GameObject frontWheelObj;
    [SerializeField] private GameObject rearWheelObj;
    [SerializeField] private FuelControl fuel;

    public Vector2 curPos;

    private PlayerControls controls;
    private Rigidbody2D frontWheelRB;
    private Rigidbody2D rearWheelRB;
    private CollisionManager frontWheelCM;
    private CollisionManager rearWheelCM;
    private bool isGrounded;
    private bool isRotating;

    private float rotationThreshold = 180f;
    private float currentRotation = 0f;
    private bool flip = false;

    void Awake()
    {
        controls = new PlayerControls();

        frontWheelRB = frontWheelObj.GetComponent<Rigidbody2D>();
        rearWheelRB = rearWheelObj.GetComponent<Rigidbody2D>();

        frontWheelCM = frontWheelObj.GetComponent<CollisionManager>();
        rearWheelCM = rearWheelObj.GetComponent<CollisionManager>();

        controls.Gameplay.Jump.performed += ctx => OnHoldAction();
        controls.Gameplay.Jump.canceled += ctx => OnReleaseAction();
    }

    private void OnEnable()
    {
        controls.Enable();

        frontWheelRB.AddTorque(-startSpeed);
        rearWheelRB.AddTorque(-startSpeed);
    }

    void Update()
    {
        if (!isGrounded && (frontWheelCM.isGrounded || rearWheelCM.isGrounded))
        {
            if (flip) fuel.curFuelAmount += fuel.maxFuelAmount * 0.1f;
            currentRotation = 0f;
            flip = false;
        }
        isGrounded = frontWheelCM.isGrounded || rearWheelCM.isGrounded;

        if (!isGrounded) CheckForFlips();

        curPos = bikeRB.transform.position;
    }

    void FixedUpdate()
    {
        if (frontWheelRB.angularVelocity > -maxSpeed)
        {
            frontWheelRB.AddTorque(-speed);
            if (frontWheelRB.angularVelocity > 0)
            {
                frontWheelRB.AddTorque(-startSpeed);
            }
        }
        if (rearWheelRB.angularVelocity > -maxSpeed)
        {
            rearWheelRB.AddTorque(-speed);
            if (rearWheelRB.angularVelocity > 0)
            {
                rearWheelRB.AddTorque(-startSpeed);
            }
        }

        if (isRotating)
        {
            bikeRB.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    private void CheckForFlips()
    {
        float rotationZ = bikeRB.transform.eulerAngles.z;

        if (rotationZ < 0) rotationZ += 360f;

        if (currentRotation < rotationThreshold && rotationZ >= rotationThreshold)
        {
            flip = true;
        }

        currentRotation = rotationZ;
    }

    private void OnHoldAction()
    {
        if (isGrounded)
        {
            bikeRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
        else
        {
            isRotating = true;
        }
    }

    private void OnReleaseAction()
    {
        isRotating = false;
    }
}
