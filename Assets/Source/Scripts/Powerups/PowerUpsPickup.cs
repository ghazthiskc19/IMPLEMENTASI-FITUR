using UnityEngine;

public class PowerUpsPickup : MonoBehaviour
{
    private PlayerPowerUpManager playerPowerUpManager;
    public PowerUpEffect powerUpEffect;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Touch");
            playerPowerUpManager = other.GetComponent<PlayerPowerUpManager>();
            if (playerPowerUpManager != null)
            {
                playerPowerUpManager.addPowerUp(powerUpEffect);
            }
            Destroy(gameObject);
        }
    }
}
