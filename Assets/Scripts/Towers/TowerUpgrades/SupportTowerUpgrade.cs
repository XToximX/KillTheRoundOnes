using UnityEngine;

public class SupportTowerUpgrade : TowerUpgrade
{
    [SerializeField] float lvl2SlowValue;

    SupportTower supportTower;

    protected override void Start()
    {
        base.Start();

        supportTower = GetComponent<SupportTower>();
    }


    protected override void Lvl1()
    {
        towerBehaviour.PernamentAddStats(lvl1BonusStats);
    }

    protected override void Lvl2()
    {
        
        supportTower.SlowValue = lvl2SlowValue;
    }

    protected override void Lvl3()
    {
        supportTower.InvokeRepeating("UpdateBuffs", 0f, 0.5f);
    }
}