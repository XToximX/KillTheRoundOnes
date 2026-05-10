using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;

public class WavesManager : MonoBehaviour
{
    public static WavesManager I;

    //Wave actions
    public event Action<SO_Wave> OnWaveStart;
    public event Action OnWaveEnd;

    [SerializeField] List<SO_Wave> waves = new List<SO_Wave>();

    private bool isWaveInProgress = false;
    private int currentWaveNumber = 0;


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
        OnWaveStart += (wave) => isWaveInProgress = true;
        OnWaveEnd += () => isWaveInProgress = false;

        EnemyManager.I.OnAllEnemiesDead += () => OnWaveEnd?.Invoke();

        OnWaveEnd += () => Debug.Log("Enemies are dead");
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !isWaveInProgress)
            StartNextWave();
    }

    public void StartNextWave()
    {
        OnWaveStart?.Invoke(waves[currentWaveNumber]);
        if (currentWaveNumber <  waves.Count - 1)
            currentWaveNumber++; // wave cap so it wont go out of index
    }
}
