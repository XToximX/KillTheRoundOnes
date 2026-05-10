using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EmptyWave", menuName = "ScriptableObjects/Wave")]
public class SO_Wave : ScriptableObject
{
    public int WaveNumber;
    public List<EnemyTypeAndCount> enemyList;
    public float SpawnInterval = 0.5f;
}

[System.Serializable]
public class EnemyTypeAndCount
{
    public GameObject EnemyPrefab;
    public int Count;
}
