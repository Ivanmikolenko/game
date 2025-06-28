using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float jumpForce = 5f;
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public Vector2 curPos;
    private Rigidbody2D rb;
    private bool isRotating;
    private bool isGrounded;

    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controls.Gameplay.Jump.performed += ctx => OnHoldAction();
        controls.Gameplay.Jump.canceled += ctx => OnReleaseAction();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnHoldAction()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
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

    void Update()
    {
        curPos = transform.position;

        Vector2 movement = new Vector2(speed, rb.linearVelocity.y);
        rb.linearVelocity = movement;
        if (isRotating)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }
}