using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    private float spawnInterval = 0.5f;

    private void OnEnable()
    {
        WavesManager.I.OnWaveStart += SpawnWave;
    }

    private void OnDisable()
    {
        WavesManager.I.OnWaveStart -= SpawnWave;
    }

    public void SpawnWave(List<SO_Enemy> wave)
    {
        StartCoroutine(SpawnEnemies(wave));
    }

    private IEnumerator SpawnEnemies(List<SO_Enemy> wave)
    {
        EnemyManager.I.isSpawningWave = true;

        WaitForSeconds waitForSeconds = new WaitForSeconds(spawnInterval);

        if(wave.Count > 0)
        {
            foreach(var e in wave)
            {
                var enemy = Instantiate(enemyPrefab, new Vector2(100f, 0f), Quaternion.identity);
                enemy.GetComponent<Enemy>().SetUp(e);
                yield return waitForSeconds;
            }
        }

        EnemyManager.I.isSpawningWave = false;
    }
}
