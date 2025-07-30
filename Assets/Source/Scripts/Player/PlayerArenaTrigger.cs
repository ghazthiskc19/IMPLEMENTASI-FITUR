using UnityEngine;

public class PlayerArenaTrigger : MonoBehaviour
{
    private IMoveable _moveableObject;
    void Start()
    {
        _moveableObject = GetComponentInParent<IMoveable>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arena"))
        {
            _moveableObject.SetIsHitWall(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Arena"))
        {
            _moveableObject.SetIsHitWall(false);
        }
    }
}
