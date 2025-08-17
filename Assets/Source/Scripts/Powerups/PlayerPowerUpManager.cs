using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPowerUpManager : MonoBehaviour
{
    public Dictionary<PowerUpsCategory, PowerUpEffect> activePowers = new();
    public Dictionary<PowerUpsCategory, Coroutine> activeCoroutine = new();

    [Header("UI Mapping")]
    public Image powerUpTankDisplay;
    public Image powerUpWeaponDisplay;
    public TMP_Text powerUpTankDuration;
    public TMP_Text powerUpWeaponDuration;
    public TMP_Text powerUpTankName;
    public TMP_Text powerUpWeaponName;
    public Sprite powerUpSprite;
    public Sprite powerUpSpriteDefault;
    private Dictionary<PowerUpsCategory, Image> displayMap;
    private Dictionary<PowerUpsCategory, TMP_Text> durationMap;
    private Dictionary<PowerUpsCategory, TMP_Text> nameMap;
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

        nameMap = new Dictionary<PowerUpsCategory, TMP_Text>
        {
            { PowerUpsCategory.tank, powerUpTankName},
            { PowerUpsCategory.weapon, powerUpWeaponName}
        };
    }
    public void AddPowerUp(GameObject obj, PowerUpEffect powerUp)
    {
        powerUpSprite = obj.GetComponent<SpriteRenderer>().sprite;
        PowerUpsCategory category = powerUp.powerUpsCategory;
        if (activePowers.ContainsKey(category))
        {
            Debug.Log($"Timpa {category} lama dengan {powerUp.name}");
            RemovePowerUp(category);
        }
        nameMap[category].text = powerUp.powerUpName;
        displayMap[category].sprite = powerUpSprite;
        activePowers.Add(category, powerUp);
        activeCoroutine.Add(category, null);

        powerUp.Apply(gameObject);
        Coroutine coroutine = StartCoroutine(PowerUpCoroutine(powerUp));
        activeCoroutine[category] = coroutine;
    }
    public void RemovePowerUp(PowerUpsCategory category)
    {
        if (activePowers.ContainsKey(category))
        {
            activePowers[category].Remove(gameObject);
            StopCoroutine(activeCoroutine[category]);
            activePowers.Remove(category);
            activeCoroutine.Remove(category);
            if (displayMap.ContainsKey(category))
            {
                displayMap[category].sprite = powerUpSpriteDefault;
                durationMap[category].text = "sec";
                nameMap[category].text = "NONE";
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
