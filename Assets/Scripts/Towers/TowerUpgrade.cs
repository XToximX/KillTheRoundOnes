using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    [SerializeField] private SO_Upgrades upgrades;

    private int dmgLvl = 0;
    private int asLvl = 0;
    private int rangeLvl = 0;

    private TowerAttack towerAttack;

    private void Start()
    {
        towerAttack = GetComponent<TowerAttack>();
    }

    public void UpgradeDmg()
    {
        if(dmgLvl < 3)
            towerAttack.RaiseDmg(upgrades.values[++dmgLvl].damage);
    }
    public void UpgradeAS()
    {
        if (asLvl < 3)
            towerAttack.RaiseAS(upgrades.values[++asLvl].attackSpeed);
    }
    public void UpgradeRange()
    {
        if (rangeLvl < 3)
            towerAttack.RaiseRange(-upgrades.values[++rangeLvl].range);
    }
}
