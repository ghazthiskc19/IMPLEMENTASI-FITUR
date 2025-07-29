using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float bulletDamage = 1;
    private Animator animator;
    private Rigidbody2D _rb;
    void Start()
    {
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        HandleCollision(other);
    }

    private void HandleCollision(Collision2D other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(bulletDamage);
        }

        if (other.gameObject.CompareTag("Arena") || playerHealth != null)
        {
            _rb.linearVelocity = Vector3.zero;
            animator.SetTrigger("IsCollision");
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 0.2f);
        }
    }
}
