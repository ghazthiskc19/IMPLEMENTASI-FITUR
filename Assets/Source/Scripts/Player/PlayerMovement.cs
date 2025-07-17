using System.Collections;
using NUnit.Framework.Constraints;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public LayerMask wallLayer;
    public float wallCheckDistance = 1;
    public float gridSize = 1f;
    public float timeToMove = 0.5f;
    public Vector2 BoxCastSize = new Vector2(1, 1);
    public float lastDirection = 0;
    public float delayTime = 0.5f;
    private float time;
    [SerializeField] private float _turnDirection;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool IsHitWall;
    [SerializeField] private bool turnDelayActive = false;
    private Vector3 originPos, targetPos;

    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue input)
    {
        _turnDirection = input.Get<float>();
    }
    void Update()
    {
        RaycastHit2D checkWallR = Physics2D.BoxCast(transform.position, BoxCastSize, 0f, transform.right, wallCheckDistance, wallLayer);
        RaycastHit2D checkWallL = Physics2D.BoxCast(transform.position, BoxCastSize, 0f, -transform.right, wallCheckDistance, wallLayer);
        Vector2 startPosR = transform.position;
        Vector2 endPosR = startPosR + ((Vector2)transform.right * wallCheckDistance);
        DrawDebugBox(startPosR, BoxCastSize, checkWallR.collider != null ? Color.red : Color.green); // Kotak awal
        DrawDebugBox(endPosR, BoxCastSize, checkWallR.collider != null ? Color.red : Color.green);   // Kotak akhir

        Vector2 startPosL = transform.position;
        Vector2 endPosL = startPosL - ((Vector2)transform.right * wallCheckDistance);
        DrawDebugBox(startPosL, BoxCastSize, checkWallL.collider != null ? Color.red : Color.green); // Kotak awal
        DrawDebugBox(endPosL, BoxCastSize, checkWallL.collider != null ? Color.red : Color.green);   // Kotak akhir


        if (_turnDirection != 0 && !turnDelayActive)
        {
            lastDirection = _turnDirection;
            _turnDirection = 0;
        }

        bool canTurnRight = lastDirection > 0 && checkWallR.collider == null;
        bool canTurnLeft = lastDirection < 0 && checkWallL.collider == null;
        if (canTurnLeft || canTurnRight)
        {
            HandleTurn();
            turnDelayActive = true;
            time = 0;
        }

        if (turnDelayActive)
        {
            time += Time.deltaTime;
            if (time >= delayTime)
            {
                turnDelayActive = false;
                time = 0;
            }
        }        

        if (!IsHitWall)
        {
            if (isMoving) return;
            MoveForward();
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arena"))
        {
            IsHitWall = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arena"))
        {
            IsHitWall = false;
        }
    }
    private void HandleTurn()
    {
        float angle = -lastDirection * 90f;
        transform.Rotate(0, 0, angle);
        lastDirection = 0;
    }
    private void MoveForward()
    {
        if (!IsHitWall)
        {
            targetPos = transform.position + transform.up;
            StartCoroutine(MovePlayer(targetPos));
        }
    }
    private IEnumerator MovePlayer(Vector3 targetPos)
    {
        isMoving = true;
        float elapsedTime = 0;
        originPos = transform.position;

        while (elapsedTime < timeToMove)
        {
            Vector3 newPos = Vector3.Lerp(originPos, targetPos, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            _rb.MovePosition(newPos);
            yield return null;
        }
        _rb.MovePosition(targetPos);
        isMoving = false;
    }
    void DrawDebugBox(Vector2 origin, Vector2 size, Color color)
    {
        Vector2 halfSize = size / 2;
        Vector2 topLeft = new Vector2(origin.x - halfSize.x, origin.y + halfSize.y);
        Vector2 topRight = new Vector2(origin.x + halfSize.x, origin.y + halfSize.y);
        Vector2 bottomLeft = new Vector2(origin.x - halfSize.x, origin.y - halfSize.y);
        Vector2 bottomRight = new Vector2(origin.x + halfSize.x, origin.y - halfSize.y);

        Debug.DrawLine(topLeft, topRight, color);
        Debug.DrawLine(topRight, bottomRight, color);
        Debug.DrawLine(bottomRight, bottomLeft, color);
        Debug.DrawLine(bottomLeft, topLeft, color);
    }
}
