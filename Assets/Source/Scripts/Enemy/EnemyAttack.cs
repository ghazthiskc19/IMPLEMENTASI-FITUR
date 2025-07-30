using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float delayAttack = 1f;
    public float bulletSpeed = 3f;
    public float time;
    public LayerMask playerLayer;
    public float detectionRange = 10f;
    public GameObject bullet;
    private bool _canAttack = true;
    private bool isAttacking;
    private EnemyHealth _enemyHealth;
    void Start()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyHealth.OnEnemyDead += HandleDeath;
    }
    void Update()
    {
        Vector2 direction = transform.up;
        RaycastHit2D playerCheck = Physics2D.Raycast(transform.position, direction, detectionRange, playerLayer);
        Debug.DrawRay(transform.position, direction * detectionRange, Color.red);

        if (playerCheck.collider != null && playerCheck.collider.CompareTag("Player"))
        {
            Debug.DrawRay(transform.position, direction * detectionRange, Color.green);
            int random = Random.Range(0, 10);
            if (random > 5) OnAttack();
        }
    }
    public void OnAttack()
    {
        if (!_canAttack) return;
        if (!isAttacking)
            StartCoroutine(DelayedAttack());
    }
    private IEnumerator DelayedAttack()
    {
        isAttacking = true;
        LaunchAttack();
        yield return new WaitForSeconds(delayAttack);
        isAttacking = false;
    }
    private void LaunchAttack()
    {
        GameObject j = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody2D rigidbody2D = j.GetComponent<Rigidbody2D>();
        rigidbody2D.linearVelocity = bulletSpeed * transform.up;

        BulletBehavior k = j.GetComponent<BulletBehavior>();
        k.targetTag = "Player";
    }
    public void HandleDeath()
    {
        _canAttack = false;
    }
}
