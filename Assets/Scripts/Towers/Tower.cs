using UnityEngine;

[RequireComponent (typeof(TowerAttack))]
[RequireComponent (typeof(TowerUpgrade))]
public class Tower : MonoBehaviour
{
    public SO_Tower towerData {  get; private set; }

    private TowerAttack towerAttack;

    public void SetUpTower(SO_Tower towerData)
    {
        this.towerData = towerData;
        towerAttack = GetComponent <TowerAttack>();
        towerAttack.SetAttackStats(towerData.AttackStats);
    }

    public void ClearTower()
    {
        transform.parent.GetComponent<TowerSlot>().ClearTower();
        Destroy(gameObject);
        return;
    }

    public void ChangeTargetingMode(TargetingMode targetingMode)
    {
        towerAttack.ChangeTargetingMode(targetingMode);
    }
}
