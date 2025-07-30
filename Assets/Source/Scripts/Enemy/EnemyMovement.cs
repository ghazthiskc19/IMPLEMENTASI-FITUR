using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyMovement : MonoBehaviour, IMoveable
{
    public LayerMask wallLayer;
    public Vector2 BoxCastSize = new Vector2(1, 1);
    public float wallCheckDistance = 1;
    public float gridSize = 1f;
    public float timeToMove = 0.5f;
    public float delayTime = 0.5f;
    [SerializeField] private float _turnDirection;
    [SerializeField] private bool _isMoving;
    [SerializeField] private bool _isHitWall;
    private Vector3 originPos, targetPos;
    private Rigidbody2D _rb;
    private EnemyHealth _enemyHealth;
    private bool _canMove = true;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyHealth.OnEnemyDead += HandleDead;
    }
    void OnMove()
    {
        bool isWallAhead = Physics2D.BoxCast(transform.position, BoxCastSize, 0f, transform.up, wallCheckDistance, wallLayer);
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

        Vector2 startPosAhead = transform.position;
        Vector2 endPosAhead = startPosAhead + ((Vector2)transform.up * wallCheckDistance);
        DrawDebugBox(startPosAhead, BoxCastSize, isWallAhead ? Color.red : Color.green);
        DrawDebugBox(endPosAhead, BoxCastSize, isWallAhead ? Color.red : Color.green);

        if (!_canMove || _isMoving) return;

        List<float> validDirection = new();
        if (checkWallL.collider == null)
        {
            validDirection.Add(-1);
        }
        if (checkWallR.collider == null)
        {
            validDirection.Add(1);
        }
        if (!isWallAhead)
        {
            validDirection.Add(0);
        }
        int randomIndex = Random.Range(0, validDirection.Count);
        if (validDirection.Count > 2) Debug.Log("Semua arah bisa");
        float choosenDirection = validDirection[randomIndex];
        HandleMove(choosenDirection);
    }
    void Update()
    {
        OnMove();
    }
    private void HandleMove(float direction)
    {
        if (direction == 0)
        {
            MoveForward();
        }
        else
        {
            float angle = -direction * 90f;
            transform.Rotate(0, 0, angle);
            MoveForward();
            
        }
    }
    private void HandleDead()
    {
        _canMove = false;
    }
    public void MoveForward()
    {
        targetPos = transform.position + transform.up;
        StartCoroutine(MoveEnemy(targetPos));
    }
    private IEnumerator MoveEnemy(Vector3 targetPos)
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
