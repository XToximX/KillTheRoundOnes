using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class TowerAttack : MonoBehaviour
{
    protected AttackStats attackStats;
    protected Enemy targetedEnemy;
    protected LayerMask enemyLayer;

    [SerializeField] protected bool drawDebugLines = true; //If on the tower will draw line to nearest enemy
    [SerializeField] TargetingMode targetingMode = TargetingMode.FIRST;

    public void SetAttackStats(AttackStats attackStats)
    {
        this.attackStats = attackStats;

        enemyLayer = LayerMask.GetMask("Enemy");

        StartCoroutine(AttackCorutine());

        InvokeRepeating(nameof(TargetEnemy), 0.1f, 0.1f);
    }

    protected abstract void HandleAttack();

    public virtual void TargetEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackStats.range);

        Enemy bestEnemy = null;

        float bestValue = float.MinValue;

        foreach(Collider2D hit in hits)
        {
            if (!hit.TryGetComponent<Enemy>(out Enemy enemy))
                continue;

            float value = 0f;

            switch(targetingMode)
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
