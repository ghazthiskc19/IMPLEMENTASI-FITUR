using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    public event Action OnPlayerDied;
    public float maxHealth = 2;
    public float currentHealth;
    public float transitionDuration = 2f;
    public bool isSheildActivate;
    public bool isAlive = true;
    public Image healtbar;
    public Color sheildColor = new(1f, 0.843f, 0f);
    public Color defaultHealthbarColor = new(1f, 0f, 0f);
    private PlayerPowerUpManager playerPowerUpManager;
    private Animator animator;

    void Start()
    {
        playerPowerUpManager = GetComponent<PlayerPowerUpManager>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    [ContextMenu("Player kena damage satu")]
    public void TakeDamageOne()
    {
        TakeDamage(1);
    }
    public void TakeDamage(float damageAmount)
    {
        if (!isAlive) return;

        if (isSheildActivate)
        {
            isSheildActivate = false;
            playerPowerUpManager.RemovePowerUp(PowerUpsCategory.tank);
            return;
        }
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            GameOver();
        }
        UpdateHealthBar(currentHealth, maxHealth);
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float percentageHealth = currentHealth / maxHealth;
        healtbar.DOFillAmount(percentageHealth, transitionDuration).SetEase(Ease.Linear);
    }

    public void ApplySheild()
    {
        isSheildActivate = true;
        healtbar.DOColor(sheildColor, 0.5f).SetEase(Ease.InOutSine);
    }
    public void RemoveSheild()
    {
        isSheildActivate = false;
        healtbar.DOColor(defaultHealthbarColor, 0.5f).SetEase(Ease.Linear);
    }
    private void GameOver()
    {
        isAlive = false;
        OnPlayerDied.Invoke();
        animator.SetTrigger("IsDeath");
    }

}
