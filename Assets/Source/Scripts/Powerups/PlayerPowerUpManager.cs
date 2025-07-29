using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.ComponentModel;

public class PlayerPowerUpManager : MonoBehaviour
{
    public Dictionary<PowerUpsCategory, PowerUpEffect> activePowers = new();
    public Dictionary<PowerUpsCategory, Coroutine> activeCoroutine = new();

    [Header("UI Mapping")]
    public Image powerUpTankDisplay;
    public Image powerUpWeaponDisplay;
    public TMP_Text powerUpTankDuration;
    public TMP_Text powerUpWeaponDuration;
    public Sprite powerUpSprite;
    public Sprite powerUpSpriteDefault;
    private Dictionary<PowerUpsCategory, Image> displayMap;
    private Dictionary<PowerUpsCategory, TMP_Text> durationMap;
    void Start()
    {
        displayMap = new Dictionary<PowerUpsCategory, Image>
        {
            { PowerUpsCategory.tank, powerUpTankDisplay},
            { PowerUpsCategory.weapon, powerUpWeaponDisplay},
        };

        durationMap = new Dictionary<PowerUpsCategory, TMP_Text>
        {
            { PowerUpsCategory.tank, powerUpTankDuration},
            { PowerUpsCategory.weapon, powerUpWeaponDuration},
        };
    }
    public void addPowerUp(GameObject obj, PowerUpEffect powerUp)
    {
        powerUpSprite = obj.GetComponent<SpriteRenderer>().sprite;
        PowerUpsCategory category = powerUp.powerUpsCategory;
        if (displayMap.ContainsKey(category))
            displayMap[category].sprite = powerUpSprite;

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
    public void RemovePowerUp(PowerUpsCategory powerUpsCategory)
    {
        if (activePowers.ContainsKey(powerUpsCategory))
        {
            StopCoroutine(activeCoroutine[powerUpsCategory]);
            activePowers[powerUpsCategory].Remove(gameObject);
            activePowers.Remove(powerUpsCategory);
            activeCoroutine.Remove(powerUpsCategory);
            if (displayMap.ContainsKey(powerUpsCategory))
            {
                displayMap[powerUpsCategory].sprite = powerUpSpriteDefault;
                durationMap[powerUpsCategory].text = "";
            }
        }
    }

    private IEnumerator PowerUpCoroutine(PowerUpEffect powerUpEffect)
    {
        float duration = powerUpEffect.duration;
        PowerUpsCategory category = powerUpEffect.powerUpsCategory;
        for (float i = duration; i > 0; i--)
        {
            if (durationMap.ContainsKey(category))
                durationMap[category].text = i + " sec";
            yield return new WaitForSeconds(1f);
        }
        RemovePowerUp(category);
    }
}
