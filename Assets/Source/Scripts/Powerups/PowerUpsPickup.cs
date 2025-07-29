using UnityEngine;

public class PowerUpsPickup : MonoBehaviour
{
    private PlayerPowerUpManager playerPowerUpManager;
    public PowerUpEffect powerUpEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPowerUpManager playerPowerUpManager = other.GetComponent<PlayerPowerUpManager>();
            if (playerPowerUpManager != null)
            {
                playerPowerUpManager.addPowerUp(gameObject, powerUpEffect);
                Destroy(gameObject);
            }
        }
    }
}
