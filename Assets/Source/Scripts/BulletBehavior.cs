using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Arena"))
        {
            Debug.Log("nabrak tembok 1");
            Destroy(gameObject);
        }
    } 
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arena"))
        {
            Debug.Log("nabrak tembok 2");
            Destroy(gameObject);
        }
    } 
}
