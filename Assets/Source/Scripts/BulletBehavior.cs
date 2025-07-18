using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float bulletDamage = 1;
    private PlayerHealth playerHealth;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Arena"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Enemy") ||
        other.gameObject.CompareTag("Player"))
        {
            playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(bulletDamage);
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Arena"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Enemy") ||
        other.gameObject.CompareTag("Player"))
        {
            playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(bulletDamage);
        }
    }
}
