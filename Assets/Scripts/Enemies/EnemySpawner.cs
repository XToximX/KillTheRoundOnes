using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

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

        foreach(var row in wave.EnemyList)
        {
            for(int i = 0; i < row.Count; i++)
            {
                var e = Instantiate(enemyPrefab);
                e.GetComponent<Enemy>().SetUp(row.EnemyData);
                yield return waitForSeconds;
            }
        }
    }
}
