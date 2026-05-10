using UnityEngine;

[RequireComponent (typeof(PathFollow))]
public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyManager.I.Register(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyEnemy()
    {
        EnemyManager.I.Unregister(gameObject);
        Destroy(gameObject);
        return;
    }
}
