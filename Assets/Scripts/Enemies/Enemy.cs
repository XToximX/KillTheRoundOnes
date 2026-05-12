using JetBrains.Annotations;
using UnityEngine;

[RequireComponent (typeof(PathFollow))]
public class Enemy : MonoBehaviour
{
    //Stats
    private int hp;
    private float speed;

    public int HP => hp;

    [HideInInspector] public PathFollow PathFollow { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyManager.I.Register(gameObject);
        PathFollow = GetComponent<PathFollow>();
    }

    public void SetUp(SO_Enemy enemyData)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = enemyData.Color;

        hp = enemyData.HP;
        speed = enemyData.Speed;

        GetComponent<PathFollow>().StartFollowingPath(enemyData);
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp < 0)
            DestroyEnemy();
    }

    public void DestroyEnemy()
    {
        EnemyManager.I.Unregister(gameObject);
        Destroy(gameObject);
        return;
    }
}
