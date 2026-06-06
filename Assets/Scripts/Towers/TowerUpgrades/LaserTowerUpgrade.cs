using UnityEngine;

public class LaserTowerUpgrade : TowerUpgrade
{
    [SerializeField] AttackStats lvl1AttackStats;
    [SerializeField] AttackStats lvl2AttackStats;


    protected override void Lvl1()
    {
        towerBehaviour.PernamentAddStats(lvl1AttackStats);
    }

    protected override void Lvl2()
    {
        towerBehaviour.PernamentAddStats(lvl2AttackStats);
    }

    protected override void Lvl3()
    {
        GetComponent<LaserTowerAttack>().EnableDamageRamping();
    }
}