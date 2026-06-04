using System.Collections.Generic;
using UnityEngine;

public class SupportTower : TowerBehaviour
{
    private LayerMask towerLayer;

    private HashSet<TowerAttack> affectedTowers = new();
    private HashSet<TowerAttack> currentTowers = new();


    [SerializeField] private AttackStats buffStats;

    public override void SetUpTower(AttackStats attackStats, TowerType towertype)
    {
        this.attackStats = attackStats;
        this.towerType = towertype;

        towerLayer = LayerMask.GetMask("Tower");

        InvokeRepeating(nameof(UpdateBuffs), 0f, 0.5f);
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
        
    }

    public void ClearBuffs()
    {
        foreach (var tower in affectedTowers)
            tower.RemoveBuff(buffStats);
    }
}
