using Mono.Cecil;
using System.Collections;
using UnityEngine;

public abstract class TowerAttack : TowerBehaviour
{
    protected Enemy targetedEnemy;
    protected LayerMask enemyLayer;

    [SerializeField] protected bool drawDebugLines = true; //If on the tower will draw line to nearest enemy
    [SerializeField] TargetingMode targetingMode = TargetingMode.FIRST;

    public override void SetUpTower(AttackStats attackStats, TowerType towerType)
    {
        this.attackStats = attackStats;
        this.towerType = towerType;

        enemyLayer = LayerMask.GetMask("Enemy");

        StartCoroutine(AttackCorutine());

        InvokeRepeating(nameof(TargetEnemy), 0.1f, 0.1f);
    }

    protected abstract void HandleAttack();

    public virtual void TargetEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, 
            new Vector2(attackStats.range, attackStats.range) * 2
            , 0f, enemyLayer);


        if (hits.Length > 0)
        {
            float bestValue = float.MinValue;
            Enemy bestEnemy = null;

            foreach (Collider2D hit in hits)
            {
                if (!hit.TryGetComponent<Enemy>(out Enemy enemy))
                    continue;

                float value = 0f;

                switch (targetingMode)
                {
                    case TargetingMode.FIRST:
                        value = enemy.PathFollow.distanceTravelled;
                        break;

                    case TargetingMode.LAST:
                        value = -enemy.PathFollow.distanceTravelled;
                        break;

                    case TargetingMode.FURTHEST:
                        value = Vector2.SqrMagnitude(transform.position - enemy.transform.position);
                        break;

                    case TargetingMode.CLOSEST:
                        value = -Vector2.SqrMagnitude(transform.position - enemy.transform.position);
                        break;

                    case TargetingMode.STRONGEST:
                        value = enemy.HP * 1000 + enemy.PathFollow.distanceTravelled;
                        break;

                    case TargetingMode.WEAKEST:
                        value = -enemy.HP * 1000 + enemy.PathFollow.distanceTravelled;
                        break;

                    default:
                        value = enemy.PathFollow.distanceTravelled;
                        break;
                }

                if (value > bestValue)
                {
                    bestEnemy = enemy;
                    bestValue = value;
                }
            }
            targetedEnemy = bestEnemy;
        }
    }

    protected void DrawDebugLines()
    {
        if (targetedEnemy != null && drawDebugLines)
        {
            Debug.DrawLine(transform.position, targetedEnemy.transform.position, Color.red);
        }
    }

    public void ChangeTargetingMode(TargetingMode newTargetingMode)
    {
        targetingMode = newTargetingMode;
    }

    protected virtual IEnumerator AttackCorutine()
    {
        WaitForSeconds wait = new WaitForSeconds(attackStats.attackSpeed);

        while (true)
        {
            yield return wait;
            HandleAttack();
        }
    }

    public void RaiseDmg(int val)
    {
        attackStats.damage += val;
    }

    public void RaiseAS(float val)
    {
        attackStats.attackSpeed += val;
    }

    public void RaiseRange(float val)
    {
        attackStats.range += val;
    }

    public void GiveBuff(AttackStats buff)
    {
        Debug.Log(attackStats.range);
        attackStats.AddStats(buff, towerType ==  TowerType.ATTACK_R);
        Debug.Log(attackStats.range);
    }

    public void RemoveBuff(AttackStats buff)
    {
        attackStats.ReduceStats(buff, towerType == TowerType.ATTACK_R);
    }
}

public enum TargetingMode
{
    FIRST,
    LAST,
    FURTHEST,
    CLOSEST,
    STRONGEST,
    WEAKEST
}
