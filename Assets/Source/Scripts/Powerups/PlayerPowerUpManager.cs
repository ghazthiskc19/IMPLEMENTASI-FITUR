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
        else
        {
            activePowers.Add(category, powerUp);
            activeCoroutine.Add(category, null);
        }

        powerUp.Apply(gameObject);
        Coroutine coroutine = StartCoroutine(PowerUpCoroutine(powerUp));
        activeCoroutine[category] = coroutine;
    }

    private IEnumerator PowerUpCoroutine(PowerUpEffect powerUpEffect)
    {
        float duration = powerUpEffect.duration;

        for (float i = duration; i > 0; i--)
        {
            Debug.Log($"{powerUpEffect.name} - {i} detik...");
            yield return new WaitForSeconds(1f);
        }
        powerUpEffect.Remove(gameObject);
        activePowers.Remove(powerUpEffect.powerUpsCategory);
        activeCoroutine.Remove(powerUpEffect.powerUpsCategory);
    }
}
