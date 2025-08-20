using UnityEngine;
using DG.Tweening;
using System;
using System.Collections;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    public event Action<GameObject> OnEnemyDead;
    public float maxHealth = 2;
    public float currentHealth;
    public float transitionDuration = 1f;
    public float durationAfterHit = 2f;
    public GameObject healthBar;
    public bool isSheildActivate;
    public bool isAlive = true;
    public Color sheildColor = new(1f, 0.843f, 0f);
    public Color defaultHealthbarColor = new(1f, 0f, 0f);
    private Animator animator;
    private Coroutine healthbarCoroutine;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetActive(false);
    }
    [ContextMenu("Enemy kena damage satu")]
    public void TakeDamageOne()
    {
        TakeDamage(1);
    }
    public void TakeDamage(float damageAmount)
    {
        if (!isAlive) return;
        if (healthbarCoroutine != null)
        {
            StopCoroutine(healthbarCoroutine);
        }
        if (isSheildActivate)
            {
                isSheildActivate = false;
                healthbarCoroutine = StartCoroutine(UpdateHealthBar());
                return;
            }
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            GameOver();
        }
        healthbarCoroutine = StartCoroutine(UpdateHealthBar());
    }

    private IEnumerator UpdateHealthBar()
    {
        healthBar.SetActive(true);
        var childSprite = healthBar.GetComponentsInChildren<SpriteRenderer>();
        foreach (var render in childSprite)
        {
            render.DOFade(1f, 0);
        }

        float percentageHealth = currentHealth / maxHealth;
        GameObject child = healthBar.transform.Find("foreground").gameObject;
        child.transform.DOScaleX(percentageHealth, transitionDuration);

        yield return new WaitForSeconds(durationAfterHit + transitionDuration);
        foreach (var render in childSprite)
        {
            render.DOFade(0f, transitionDuration);
        }
        yield return new WaitForSeconds(transitionDuration);
        healthBar.SetActive(false);
    }

    private void GameOver()
    {
        isAlive = false;
        OnEnemyDead?.Invoke(gameObject);
        animator.SetTrigger("IsDeath");
        Destroy(gameObject, 2f);
    }
}
