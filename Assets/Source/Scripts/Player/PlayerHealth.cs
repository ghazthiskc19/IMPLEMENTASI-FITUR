using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 2;
    public float currentHealth;
    public bool isSheildActivate;
    public void Onable()
    {
        EventManager.instance.action += TakeDamage;
    }
    public void Osable()
    {
        EventManager.instance.action -= TakeDamage;
    }
    public void TakeDamage(float damageAmount)
    {
        if (isSheildActivate)
        {
            isSheildActivate = false;
            return;
        }
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {

    }

}
