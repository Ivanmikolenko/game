using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float jumpForce = 5f;
    [SerializeField] public float speed = 500f;
    [SerializeField] public float rotationSpeed = 200f;
    [SerializeField] private Rigidbody2D bikeRB;
    [SerializeField] private Rigidbody2D frontWheelRB;
    [SerializeField] private Rigidbody2D rearWheelRB;

    public Vector2 curPos;

    private float moveInput;

    void Update()
    {
        curPos = transform.position;
        frontWheelRB.AddTorque(-speed * Time.fixedDeltaTime);
        rearWheelRB.AddTorque(-speed * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        frontWheelRB.AddTorque(-moveInput * speed * Time.fixedDeltaTime);
        rearWheelRB.AddTorque(-moveInput * speed * Time.fixedDeltaTime);
        bikeRB.AddTorque(moveInput * rotationSpeed * Time.fixedDeltaTime);
    }
}