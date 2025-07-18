using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Arena"))
        {
            Debug.Log("nabrak tembok 1");
            Destroy(gameObject);
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Arena"))
        {
            Debug.Log("nabrak tembok 1");
            Destroy(gameObject);
        }
    }
}
