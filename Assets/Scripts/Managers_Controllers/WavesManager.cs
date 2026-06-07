using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;
using TMPro;

using Random = UnityEngine.Random;
using UnityEngine.UI;

public class WavesManager : MonoBehaviour
{
    public static WavesManager I;

    [SerializeField] private List<SO_Enemy> enemyPool;
    private List<SO_Enemy> enemiesInWave = new();

    //Wave actions
    public event Action<List<SO_Enemy>> OnWaveStart;
    public event Action OnWaveEnd;

    private bool isWaveInProgress = false;
    private int currentWave = 0;

    [SerializeField] private TextMeshProUGUI waveDisplay;
    [SerializeField] private Image waveUIButton;
    [SerializeField] private Sprite[] buttonSprites; //0 play - 1 pause
    private bool currentsprite = false;

    public int CurrentWave => currentWave;
    public bool IsWaveInProgress => isWaveInProgress;

    [Header("ForDebug")]
    [SerializeField] bool debuging = false;
    [SerializeField] int startingWave;

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }

        I = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyManager.I.OnAllEnemiesDead += EndWave;

        if (debuging)
            currentWave = startingWave;

        GenerateWave(currentWave + 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            OnButtonPressed();
    }

    public void StartWave()
    {
        isWaveInProgress = true;
        currentWave++;
        UpdateWaveDisplay();
        SwitchButtonSprite();

        OnWaveStart?.Invoke(enemiesInWave);
    }

    public void EndWave()
    {
        isWaveInProgress = false;
        GenerateWave(currentWave + 1);
        ResourcesManager.I.AddCoins(1);
        SwitchButtonSprite();


        OnWaveEnd?.Invoke();
    }

    private void GenerateWave(int waveIndex)
    {
        int budget = Mathf.RoundToInt(
            5 + 
            waveIndex * 2 + 
            Mathf.Pow(waveIndex, 1.1f)
            );


        List<SO_Enemy> validEnemies = enemyPool.FindAll(e => e.MinWave <= waveIndex);
        enemiesInWave.Clear();

        while(budget > 0)
        {
            List<SO_Enemy> affordable = validEnemies.FindAll(e => e.WaveCost <= budget);

            if (affordable.Count == 0)
                break;

            var weightedEnemy = GetWeightEnemy(affordable);
            enemiesInWave.Add(weightedEnemy);
            budget -= weightedEnemy.WaveCost;
        }
    }

    private SO_Enemy GetWeightEnemy(List<SO_Enemy> enemies)
    {
        float totalWeight = 0;

        foreach (var e in enemies)
            totalWeight += e.SpawnWeight;

        float randomWeight = Random.Range(0, totalWeight);

        float current = 0;

        foreach(var e in enemies)
        {
            current += e.SpawnWeight;

            if (randomWeight <= current)
                return e;
        }

        return enemies[0];
    }

    private void UpdateWaveDisplay()
    {
        waveDisplay.text = $"Wave: {currentWave}";
    }

    public void PauseWave()
    {
        SwitchButtonSprite();
        TimeManager.I.PauseTime();
    }

    private void SwitchButtonSprite()
    {
        if (currentsprite)
        {
            waveUIButton.sprite = buttonSprites[0];
        }
        else
            waveUIButton.sprite = buttonSprites[1];

        currentsprite = !currentsprite;
    }

    public void OnButtonPressed()
    {
        if (!isWaveInProgress)
            StartWave();
        else
            PauseWave();
    }
}
