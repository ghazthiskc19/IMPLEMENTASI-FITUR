using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float turnSpeed = 200f;
    public LayerMask wallLayer;
    public float wallCheckDistance = 0.5f;
    private float _moveDirection;
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = transform.up * movementSpeed;
        HandleTurn();
    }
    void HandleTurn()
    {
        bool checkWallL = Physics2D.Raycast(transform.position, -transform.right, wallLayer);
        bool checkWallR = Physics2D.Raycast(transform.position, transform.right, wallLayer);

        
    }
    public void SetMoveDirection(float moveDirection)
    {
        _moveDirection = moveDirection;
    }
}
