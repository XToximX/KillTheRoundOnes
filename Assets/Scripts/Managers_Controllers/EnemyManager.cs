using UnityEngine;
using System.Collections.Generic;
using System;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager I;

    public event Action OnAllEnemiesDead;

    private List<GameObject> enemiesLiving = new List<GameObject>();

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }

        I = this;
    }

    public void Register(GameObject enemy)
    {
        enemiesLiving.Add(enemy);
    }

    public void Unregister(GameObject enemy)
    {
        enemiesLiving.Remove(enemy);

        if (enemiesLiving.Count == 0)
            OnAllEnemiesDead?.Invoke();
    }

}
