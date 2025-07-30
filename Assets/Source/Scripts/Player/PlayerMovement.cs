using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour, IMoveable
{
    public LayerMask wallLayer;
    public Vector2 BoxCastSize = new Vector2(1, 1);
    public float wallCheckDistance = 1;
    public float gridSize = 1f;
    public float timeToMove = 0.5f;
    public float originalSpeed = .5f;
    public float lastDirection = 0;
    public float delayTime = 0.5f;
    private float time;
    [SerializeField] private float _turnDirection;
    [SerializeField] private bool _isMoving;
    [SerializeField] private bool _isHitWall;
    [SerializeField] private bool _turnDelayActive = false;
    private Vector3 originPos, targetPos;

    private Rigidbody2D _rb;
    private PlayerHealth _playerHealth;
    private bool _canMove = true;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerHealth.OnPlayerDied += HandleDead;
    }

    public void OnMove(InputValue input)
    {
        _turnDirection = input.Get<float>();
    }
    void Update()
    {
        if (!_canMove)
        {
            return;
        }
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

        if (_turnDirection != 0 && !_turnDelayActive)
        {
            lastDirection = _turnDirection;
            _turnDirection = 0;
        }

        bool canTurnRight = lastDirection > 0 && checkWallR.collider == null;
        bool canTurnLeft = lastDirection < 0 && checkWallL.collider == null;
        if (canTurnLeft || canTurnRight)
        {
            HandleTurn();
            _turnDelayActive = true;
            time = 0;
        }

        if (_turnDelayActive)
        {
            time += Time.deltaTime;
            if (time >= delayTime)
            {
                _turnDelayActive = false;
                time = 0;
            }
        }
        if (!_isHitWall)
        {
            if (_isMoving) return;
            MoveForward();
        }
    }
    private void HandleTurn()
    {
        float angle = -lastDirection * 90f;
        transform.Rotate(0, 0, angle);
        lastDirection = 0;
    }
    private void HandleDead()
    {
        _canMove = false;
    }
    public void MoveForward()
    {
        if (!_isHitWall)
        {
            targetPos = transform.position + transform.up;
            StartCoroutine(MovePlayer(targetPos));
        }
    }
    private IEnumerator MovePlayer(Vector3 targetPos)
    {
        _isMoving = true;
        originPos = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < timeToMove)
        {
            elapsedTime += Time.deltaTime;
            Vector3 newPos = Vector3.Lerp(originPos, targetPos, elapsedTime / timeToMove);
            _rb.MovePosition(newPos);
            yield return null;
        }
        _rb.MovePosition(targetPos);
        _isMoving = false;
    }
    public void SetIsHitWall(bool state)
    {
        _isHitWall = state;
    }
    public void ChangeSpeed(float speedUpMultiplier)
    {
        timeToMove /= speedUpMultiplier;
        delayTime = .5f;
    }
    public void OriginalSpeed()
    {
        timeToMove = originalSpeed;
        delayTime = .5f;
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