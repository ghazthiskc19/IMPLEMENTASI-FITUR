using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUpManager : MonoBehaviour
{
    public Dictionary<PowerUpsCategory, PowerUpEffect> activePowers = new();
    public Dictionary<PowerUpsCategory, Coroutine> activeCoroutine = new();
    public void addPowerUp(PowerUpEffect powerUp)
    {
        PowerUpsCategory category = powerUp.powerUpsCategory;

        if (activePowers.ContainsKey(category))
        {
            Debug.Log($"Menimpa power-up {category} lama dengan {powerUp.name}");
            StopCoroutine(activeCoroutine[category]);
            activePowers[category] = powerUp;
        }

        powerUp.Apply(gameObject);
        activePowers[category] = powerUp;

        if (powerUp.duration > 0)
        {
            Coroutine coroutine = StartCoroutine(PowerUpCoroutine(powerUp));
            activeCoroutine[category] = coroutine;
        }
        powerUp.Apply(gameObject);
    }

    private IEnumerator PowerUpCoroutine(PowerUpEffect powerUpEffect)
    {
        yield return new WaitForSeconds(powerUpEffect.duration);
        activePowers.Remove(powerUpEffect.powerUpsCategory);
        activeCoroutine.Remove(powerUpEffect.powerUpsCategory);
    }


}
