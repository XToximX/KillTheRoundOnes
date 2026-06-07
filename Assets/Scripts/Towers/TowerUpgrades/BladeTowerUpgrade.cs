using UnityEngine;

public class BladeTowerUpgrade : TowerUpgrade
{
    
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
        towerBehaviour.PernamentAddStats(lvl3BonusStats);
        lvl3Blade.SetActive(true);
    }
}
