using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public abstract class TowerAttack : MonoBehaviour
{
    private AttackStats attackStats;
    private Enemy targetedEnemy;
    private LayerMask enemyLayer;

    [SerializeField] bool drawDebugLines = true; //If on the tower will draw line to nearest enemy
    [SerializeField] TargetingMode targetingMode = TargetingMode.FIRST;

    public void SetAttackStats(AttackStats attackStats)
    {
        this.attackStats = attackStats;

        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    public abstract void HandleAttack();

    public virtual void TargetEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackStats.range);

        Enemy bestEnemy = null;

        targetedEnemy = null;

        float bestValue = float.MinValue;

        foreach(Collider2D hit in hits)
        {
            if (!hit.TryGetComponent<Enemy>(out Enemy enemy))
                continue;

            float value = 0f;

            switch(targetingMode)
            {
                case TargetingMode.FIRST:
                    value = enemy.PathFollow.DistanceTravelled;
                    break;

                case TargetingMode.LAST:
                    value = -enemy.PathFollow.DistanceTravelled;
                    break;

                case TargetingMode.FURTHEST:
                    value = Vector2.SqrMagnitude(transform.position - enemy.transform.position);
                    break;

                case TargetingMode.CLOSEST:
                    value = -Vector2.SqrMagnitude(transform.position - enemy.transform.position);
                    break;

                case TargetingMode.STRONGEST:
                    value = enemy.HP * 1000 + enemy.PathFollow.DistanceTravelled;
                    break;

                case TargetingMode.WEAKEST:
                    value = -enemy.HP * 1000 + enemy.PathFollow.DistanceTravelled;
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

    private void Update()
    {
        if (targetedEnemy != null && drawDebugLines)
        {
            Debug.DrawLine(transform.position, targetedEnemy.transform.position, Color.red);
        }
    }

    private void FixedUpdate()
    {
        TargetEnemy();
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
