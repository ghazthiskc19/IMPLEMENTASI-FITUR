using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public event Action OnEnemySpawnReq;
    public StatisticsSO stat;
    public int remainingToSpawn;
    public float durationSpawn;
    public GameObject[] enemyVariant;
    public GameManager gameManager;
    public Dictionary<GameObject, GameObject> spawnerEnemyPair = new();
    public GameObject UncomingEnemyWrapper;
    public TMP_Text remainingEnemy;
    public TMP_Text EnemyDurationText;
    public TMP_Text waveDescription;
    private float spawnTimer;
    private bool spawnCountdownActive = false;

    void Awake()
    {
        foreach (Transform t in transform)
        {
            spawnerEnemyPair.Add(t.gameObject, null);
        }
    }
    void Start()
    {
        OnEnemySpawnReq += StartSpawnCountdown;
        gameManager.OnClearWave += StartNextWave;
        SpawnEnemy();
        UpdateHUD();
        UncomingEnemyWrapper.SetActive(false);
    }
    void Update()
    {
        if (spawnCountdownActive)
        {
            spawnTimer += Time.deltaTime;
            float remainingTime = Mathf.Max(0f, durationSpawn - spawnTimer);
            EnemyDurationText.text = $"{remainingTime:F1} s";
            UncomingEnemyWrapper.SetActive(true);
            if (stat.enemyKillCount >= stat.enemyMax)
            {
                spawnCountdownActive = false;
                spawnTimer = 0f;
                UncomingEnemyWrapper.SetActive(false);
                EnemyDurationText.text = "";
            }

            if (spawnTimer > durationSpawn)
            {
                spawnCountdownActive = false;
                spawnTimer = 0f;
                UncomingEnemyWrapper.SetActive(false);
                EnemyDurationText.text = "";

                SpawnEnemy();
                UpdateHUD();
            }
        }
    }
    private void StartSpawnCountdown()
    {
        if (spawnCountdownActive || remainingToSpawn <= 0) return;
        spawnCountdownActive = true;
        remainingToSpawn = 6;
        spawnTimer = 0f;
    }
    private void StartNextWave()
    {
        int waveIndex = gameManager.currentWave;
        waveDescription.text = "WAVE " + waveIndex + " CLEAR";
        spawnCountdownActive = false;
        spawnTimer = 0f;
        UncomingEnemyWrapper.SetActive(false);
        EnemyDurationText.text = "";
        StartCoroutine(StartNextWaveAnimation());
    }

    private IEnumerator StartNextWaveAnimation()
    {
        RectTransform rt = waveDescription.GetComponent<RectTransform>();
        rt.DOAnchorPos(new Vector3(-150, 0, 0), 1f);
        yield return new WaitForSeconds(3f);
        rt.DOAnchorPos(new Vector3(-150, -800, 0), 1f);
        yield return new WaitForSeconds(1f);
        stat.enemyKillCount = 0;
        yield return new WaitForSeconds(2f);
        SpawnEnemy();
    }
    private void SpawnEnemy()
    {
        int count = 0;
        foreach (var spawner in spawnerEnemyPair.Keys.ToList())
        {
            if (remainingToSpawn <= 0) break;
            if (spawnerEnemyPair[spawner] == null && stat.enemyKillCount + count < stat.enemyMax)
            {
                GameObject enemy = DeployEnemy(gameManager.currentWave, spawner.transform);
                if (enemy == null) return;

                EnemyHealth health = enemy.GetComponent<EnemyHealth>();
                if (health != null)
                    health.OnEnemyDead += RemoveEnemyOnDead;

                spawnerEnemyPair[spawner] = enemy;
                remainingToSpawn--;
                count++;
            }
        }
        UpdateHUD();
    }
    public void RemoveEnemyOnDead(GameObject enemy)
    {
        if (enemy == null) return;
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        enemyHealth.OnEnemyDead -= RemoveEnemyOnDead;

        var kv = spawnerEnemyPair.FirstOrDefault(x => x.Value == enemy);
        if (kv.Key != null) spawnerEnemyPair[kv.Key] = null;
    
        stat.enemyKillCount++;
        remainingToSpawn++;
        if (remainingToSpawn > 2) OnEnemySpawnReq?.Invoke();
        UpdateHUD();
    }
    private void UpdateHUD()
    {
        remainingEnemy.text = Mathf.Max(0, stat.enemyMax - stat.enemyKillCount) + " enemy left";
    }
    private GameObject DeployEnemy(int enemyLevel, Transform spawnerPos)
    {
        GameObject enemy = enemyVariant[enemyLevel - 1];
        return Instantiate(enemy, spawnerPos.transform.position, Quaternion.identity, spawnerPos);
    }
    public void InstaKillEnemy()
    {
        if (spawnerEnemyPair.Count == 0) return;

        EnemyHealth enemyHealth;
        foreach (var enemy in spawnerEnemyPair.Values.ToList())
        {
            if (enemy != null)
            {
                enemyHealth = enemy.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(enemyHealth.maxHealth);
                return;
            }
        }
    }
}
