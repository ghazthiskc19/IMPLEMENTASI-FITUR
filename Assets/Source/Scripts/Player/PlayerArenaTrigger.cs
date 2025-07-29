using UnityEngine;

public class PlayerArenaTrigger : MonoBehaviour
{
    private PlayerMovement playerMovement;
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arena"))
        {
            playerMovement.SetIsHitWall(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Arena"))
        {
            playerMovement.SetIsHitWall(false);
        }
    }
}
