using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EmptyWave", menuName = "ScriptableObjects/Wave")]
public class SO_Wave : ScriptableObject
{
    [SerializeField] int waveNumber;
    [SerializeField] List<EnemyTypeAndCount> enemyList;
    [SerializeField] float spawnInterval = 0.5f;

    public int WaveNumber => waveNumber;
    public List<EnemyTypeAndCount> EnemyList => enemyList;
    public float SpawnInterval => spawnInterval;
}


[System.Serializable]
public class EnemyTypeAndCount
{
    public SO_Enemy EnemyData;
    public int Count = 1;
}
