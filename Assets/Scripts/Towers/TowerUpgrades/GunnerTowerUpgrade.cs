using UnityEngine;

public class GunnerTowerUpgrade : TowerUpgrade
{
    GunnerTowerAttack towerAttack;

    protected override void Start()
    {
        base.Start();

        towerAttack = GetComponent<GunnerTowerAttack>();
    }

    protected override void Lvl1()
    {
        towerBehaviour.PernamentAddStats(lvl1BonusStats);
    }

    protected override void Lvl2()
    {
        towerAttack.SetPenetration(2);
        towerBehaviour.PernamentAddStats(lvl2BonusStats);
    }

    protected override void Lvl3()
    {
        towerAttack.SetPenetration(4);
        towerAttack.SetDamageFallof(0.1f);
    }
}