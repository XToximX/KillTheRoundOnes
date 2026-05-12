using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof(TowerAttack))]
public class Tower : MonoBehaviour
{
    private SO_Tower towerData;

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
