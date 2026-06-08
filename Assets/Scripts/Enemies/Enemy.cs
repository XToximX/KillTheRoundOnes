using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof(PathFollow))]
public class Enemy : MonoBehaviour
{
    //Stats
    private float hp;
    private float baseHp;
    public float Speed;
    private float baseSpeed;
    private SO_Enemy enemyData;

    private bool isDebuffed = false;
    private float currentDebuff = 0f;

    private bool canDie = false;

    public float HP => hp;
    public float BaseHP => baseHp;

    public PathFollow PathFollow { get; private set; }


    private void Awake()
    {
        PathFollow = GetComponent<PathFollow>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyManager.I.Register(gameObject);
    }

    public void SetUp(SO_Enemy enemyData)
    {
        this.enemyData = enemyData;

        transform.localScale = Vector3.one * enemyData.Size;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = enemyData.Color;

        hp = enemyData.HP * EnemyManager.I.GlobalHPMultiplyer;
        baseHp = hp;
        Speed = enemyData.Speed;
        baseSpeed = Speed;
        PathFollow.StartFollowingPath();

        canDie = true;
    }

    public bool Debuff(float value)
    {
        if (currentDebuff < value)
        {
            currentDebuff = value;
            Speed *= (1f - value);
            return true;
        }

        if (isDebuffed)
            return false;

        Speed *= (1f - value);
        isDebuffed = true;

        return true;
    }

    public void RemoveDebuff()
    {
        isDebuffed = false;
        Speed = baseSpeed;
    }

    public void TakeDamage(float dmg)
    {
        if (!canDie)
            return; 
        
        hp -= dmg;

        if (hp <= 0)
        {
            AudioManager.I.PlaySFX(enemyData.DeathClip);
            DestroyEnemy();
        }
    }

    public void DestroyEnemy()
    {
        EnemyManager.I.Unregister(gameObject);
        //ResourcesManager.I.AddCoins(enemyData.CoinValue);
        Destroy(gameObject);
        return;
    }

    public void PathEnd()
    {
        ResourcesManager.I.TakeDamage(enemyData.Dmg);
        DestroyEnemy();
        return;
    }
}
