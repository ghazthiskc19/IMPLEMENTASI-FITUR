using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float bulletDamage = 1;
    public string targetTag;
    private Animator animator;
    private Rigidbody2D _rb;
    void Start()
    {
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        HandleCollision(other);
    }

    private void HandleCollision(Collider2D other)
    {
        bool hitValidTarget = false;
        if (other.CompareTag(targetTag))
        {
            IDamagable damagableObject = other.gameObject.GetComponent<IDamagable>();
            if (damagableObject != null)
            {
                Debug.Log("Kena");
                damagableObject.TakeDamage(bulletDamage);
                hitValidTarget = true;

            }
        }
        if (hitValidTarget || other.CompareTag("Arena"))
        {
            DestroyBullet();
        }
    }
    public void DestroyBullet()
    {
        _rb.linearVelocity = Vector3.zero;
        animator.SetTrigger("IsCollision");
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.2f);
    }
}
