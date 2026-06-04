using System.Collections.Generic;
using UnityEngine;

public class SupportTower : TowerBehaviour
{
    private LayerMask towerLayer;
    private LayerMask enemyLayer;

    private HashSet<TowerAttack> affectedTowers = new();
    private HashSet<TowerAttack> currentTowers = new();

    private HashSet<Enemy> affectedEnemies = new();
    private HashSet<Enemy> currentEnemies = new();

    [SerializeField] private AttackStats buffStats;
    public float SlowValue;

    public override void SetUpTower(AttackStats attackStats, TowerType towertype)
    {
        this.attackStats = attackStats;
        this.towerType = towertype;

        towerLayer = LayerMask.GetMask("Tower");
        enemyLayer = LayerMask.GetMask("Enemy");

        //InvokeRepeating(nameof(UpdateBuffs), 0f, 0.5f); <-- moved to lvl3 upgrade
        InvokeRepeating(nameof(UpdateDebuffs), 0f, 0.1f);

    }

    private void UpdateBuffs()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position,
            new Vector2(attackStats.range, attackStats.range) * 2
            , 0f, towerLayer);

        currentTowers.Clear();

        foreach (Collider2D hit in hits)
        {
            if(hit.TryGetComponent<TowerAttack>(out TowerAttack tower))
            {
                currentTowers.Add(tower);

                if(!affectedTowers.Contains(tower))
                    tower.GiveBuff(buffStats);
            }
        }

        foreach(var tower in affectedTowers)
        {
            if (!currentTowers.Contains(tower))
                tower.RemoveBuff(buffStats);
        }

        affectedTowers = new HashSet<TowerAttack>(currentTowers);
    }

    private void UpdateDebuffs()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position,
            new Vector2(attackStats.range, attackStats.range) * 2
            , 0f, enemyLayer);

        currentEnemies.Clear();

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<Enemy>(out Enemy enemy))
            {
                currentEnemies.Add(enemy);
                enemy.Debuff(SlowValue);
            }
        }

        foreach (Enemy enemy in affectedEnemies)
        {
            if (!currentEnemies.Contains(enemy))
                enemy.RemoveDebuff();
        }

        affectedEnemies = new HashSet<Enemy>(currentEnemies);
    }

    public void ClearBuffs()
    {
        foreach (var tower in affectedTowers)
            tower.RemoveBuff(buffStats);

        foreach (Enemy enemy in affectedEnemies)
            enemy.RemoveDebuff();
    }
}
