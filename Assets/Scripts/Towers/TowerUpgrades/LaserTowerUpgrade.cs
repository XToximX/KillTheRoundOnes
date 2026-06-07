using UnityEngine;

public class LaserTowerUpgrade : TowerUpgrade
{


    protected override void Lvl1()
    {
        towerBehaviour.PernamentAddStats(lvl1BonusStats);
    }

    protected override void Lvl2()
    {
        towerBehaviour.PernamentAddStats(lvl2BonusStats);
    }

    protected override void Lvl3()
    {
        GetComponent<LaserTowerAttack>().EnableDamageRamping();
    }
}