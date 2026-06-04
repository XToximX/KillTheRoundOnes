using UnityEngine;

public class BladeTowerUpgrade : TowerUpgrade
{
    [SerializeField] AttackStats lvl1BonusStats;
    [SerializeField] AttackStats lvl2BonusStats;
    [SerializeField] GameObject lvl3Blade;

    protected override void Lvl1()
    {
        towerBehaviour.PernamentAddStats(lvl1BonusStats);
    }

    protected override void Lvl2()
    {
        towerBehaviour.PernamentAddStats(lvl2BonusStats);
        GetComponent<BladeTowerAttack>().SetBlades();
    }

    protected override void Lvl3()
    {
        lvl3Blade.SetActive(true);
    }
}
