using UnityEngine;

public class GunnerTowerUpgrade : TowerUpgrade
{
    GunnerTowerAttack towerAttack;

    [SerializeField] AttackStats lvl2AttackStats;

    protected override void Start()
    {
        base.Start();

        towerAttack = GetComponent<GunnerTowerAttack>();
    }

    protected override void Lvl1()
    {
        towerAttack.SetPenetration(2);
    }

    protected override void Lvl2()
    {
        towerBehaviour.PernamentAddStats(lvl2AttackStats);
    }

    protected override void Lvl3()
    {
        towerAttack.SetPenetration(3);
        towerAttack.SetDamageFallof(0.1f);
    }
}