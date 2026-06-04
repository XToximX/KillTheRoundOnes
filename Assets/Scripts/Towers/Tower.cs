using UnityEngine;

[RequireComponent(typeof(TowerBehaviour))]
public class Tower : MonoBehaviour
{
    public SO_Tower towerData { get; private set; }

    private TowerBehaviour towerBehavoiur;

    public TowerType towerType;

    private void Awake()
    {
        if(GetComponent<TowerUpgrade>() == null)
        {
            Debug.LogError($"{name} is missing TowerUpgrade component");
        }
    }

    public void SetUpTower(SO_Tower towerData)
    {
        this.towerData = towerData;

        towerType = towerData.towerType;
        towerBehavoiur = GetComponent<TowerBehaviour>();
        towerBehavoiur.SetUpTower(towerData.AttackStats, towerType);
    }

    public void ClearTower()
    {
        transform.parent.GetComponent<TowerSlot>().ClearTower();

        if (towerType == TowerType.SUPPORT)
            GetComponent<SupportTower>().ClearBuffs();

        Destroy(gameObject);
        return;
    }

}

public enum TowerType
{
    ATTACK_R,
    ATTACK_M,
    SUPPORT
}

public abstract class TowerBehaviour : MonoBehaviour
{
    protected AttackStats attackStats;
    protected TowerType towerType;

    public abstract void SetUpTower(AttackStats attackStats, TowerType towerType);

    public AttackStats GetAttackStats()
    {
        return attackStats;
    }
}