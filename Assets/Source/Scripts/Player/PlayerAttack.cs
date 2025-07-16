using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float duration = 1f;
    public float bulletSpeed = 5f;
    public GameObject bullet;
    void Start()
    {

    }

    public void LaunchAttack()
    {
        Vector2 direction = Vector2.up;
        GameObject j = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody2D rigidbody2D = j.GetComponent<Rigidbody2D>();
        rigidbody2D.linearVelocity = bulletSpeed * Time.deltaTime * direction;
    }
}
