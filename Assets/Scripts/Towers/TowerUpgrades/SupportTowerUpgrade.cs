using UnityEngine;

public class SupportTowerUpgrade : TowerUpgrade
{
    [SerializeField] AttackStats lvl1AttackStats;
    [SerializeField] float lvl2SlowValue;

    SupportTower supportTower;


    protected override void Lvl1()
    {
        towerBehaviour.PernamentAddStats(lvl1AttackStats);
    }

    protected override void Lvl2()
    {
        
        GetComponent<SupportTower>().SlowValue = lvl2SlowValue;
    }

    protected override void Lvl3()
    {
        GetComponent<SupportTower>().InvokeRepeating("UpdateBuffs", 0f, 0.5f);
    }
}