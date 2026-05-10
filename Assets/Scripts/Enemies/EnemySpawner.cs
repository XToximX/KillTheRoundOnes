using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    private void Start()
    {
        WavesManager.I.OnWaveStart += SpawnWave;
    }

    public void SpawnWave(SO_Wave wave)
    {
        StartCoroutine(SpawnEnemies(wave));
    }

    private IEnumerator SpawnEnemies(SO_Wave wave)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(wave.SpawnInterval);

        foreach(var row in wave.enemyList)
        {
            for(int i = 0; i < row.Count; i++)
            {
                Instantiate(row.EnemyPrefab);
                yield return waitForSeconds;
            }
        }
    }
}
