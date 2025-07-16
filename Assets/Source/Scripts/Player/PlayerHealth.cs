using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 1000;
    public float currentHealth = 1000;
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
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    public void TakeHeal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {

    }

}
