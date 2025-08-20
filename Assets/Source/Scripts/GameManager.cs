using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System;
using TMPro;
public class GameManager : MonoBehaviour
{
    public event Action OnClearWave;
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;
    public StatisticsSO stat;
    public Image overlayMenu;
    public RectTransform gameOverPopUp;
    public float duration;
    public float delay;
    public int currentWave;
    [Header("Assets For Win Animation")]
    public GameObject overlayWin;
    public TMP_Text[] winText;
    public CanvasGroup winButton;
    private Image img;

    void Start()
    {
        playerHealth.OnPlayerDied += DisplayDeathPopUp;
        stat.enemyKillCount = 0;
        foreach (var text in winText)
        {
            text.DOFade(0, 0);
        }
        img = overlayWin.GetComponent<Image>();
        img.DOFade(0, 0);
        winButton.DOFade(0, 0);
        overlayWin.SetActive(false);
    }
    void Update()
    {
        if (stat.enemyKillCount >= stat.enemyMax)
        {
            Debug.Log("Masuk sini gak bre?");
            if (currentWave == 4)
            {
                DisplayWinPopUp();
                return;
            }
            OnClearWave?.Invoke();
            currentWave++;
        }
        switch (currentWave)
        {
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
        }
        ;
    }
    private void DisplayDeathPopUp()
    {
        StartCoroutine(DisplayPopUp());
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

    [ContextMenu("Trigger Win Animation")]
    private void DisplayWinPopUp()
    {
        StartCoroutine(WinAnimation());
    }

    private IEnumerator WinAnimation()
    {
        overlayWin.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        playerMovement.enabled = false;
        playerAttack.enabled = false;
        img.DOFade(1, 3f);

        yield return new WaitForSeconds(6f);

        float delay = 0;
        foreach (var text in winText)
        {
            text.DOFade(1, 1f).SetDelay(delay);
            delay += .3f;
        }

        yield return new WaitForSeconds(2f);

        winButton.DOFade(1, 1f);
    }

}
