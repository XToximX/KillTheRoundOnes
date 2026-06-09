using UnityEngine;
using System.Collections.Generic;
using System;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager I;

    public event Action OnAllEnemiesDead;

    private List<GameObject> enemiesLiving = new List<GameObject>();

    public bool isSpawningWave = false;

    public float GlobalHPMultiplyer = 1f;

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }

        I = this;
    }

    private void Start()
    {
        WavesManager.I.OnWaveEnd += CalculateHPMulitier;
    }

    public void Register(GameObject enemy)
    {
        enemiesLiving.Add(enemy);
    }

    public void Unregister(GameObject enemy)
    {
        if(enemiesLiving.Contains(enemy))
            enemiesLiving.Remove(enemy);

        if (enemiesLiving.Count == 0 && !isSpawningWave)
            OnAllEnemiesDead?.Invoke();

    }

    private void CalculateHPMulitier()
    {
        //GlobalHPMultiplyer = Mathf.Min(
        //    20f,
        //    Mathf.Pow(1.06f, WavesManager.I.CurrentWave)
        //    );

        Mathf.Pow(1.06f, WavesManager.I.CurrentWave);
    }
}
