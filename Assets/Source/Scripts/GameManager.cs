using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System;
public class GameManager : MonoBehaviour
{
    public event Action OnClearWave;
    public PlayerHealth playerHealth;
    public StatisticsSO stat;
    public Image overlayMenu;
    public RectTransform gameOverPopUp;
    public float duration;
    public float delay;
    public int currentWave;
    void Start()
    {
        playerHealth.OnPlayerDied += DisplayDeathPopUp;
        stat.enemyKillCount = 0;
    }
    void Update()
    {
        if (stat.enemyKillCount >= stat.enemyMax)
        {
            Debug.Log("Masuk sini gak bre?");
            OnClearWave?.Invoke();
            currentWave++;
        }

        switch (currentWave) { 
            case 1:
                stat.enemyMax = 10;
            break;
            case 2:
                stat.enemyMax = 13;
            break;
            case 3:
                stat.enemyMax = 16;
            break;
            case 4:
                stat.enemyMax = 20;
            break;
        };
    }
    private void DisplayDeathPopUp()
    {
        StartCoroutine(DisplayPopUp());
        OnClearWave?.Invoke();
    }
    private IEnumerator DisplayPopUp()
    {
        overlayMenu.enabled = true;
        yield return new WaitForSeconds(delay - .1f);
        overlayMenu.DOFade(0.5f, duration);
        yield return new WaitForSeconds(delay);
        gameOverPopUp.DOAnchorPos(Vector2.zero, duration);
        yield return new WaitForSeconds(delay + 1f);
        Time.timeScale = 0f;
    }

}
