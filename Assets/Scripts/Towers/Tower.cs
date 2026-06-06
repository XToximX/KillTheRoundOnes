using UnityEngine;

[RequireComponent(typeof(TowerBehaviour))]
public class Tower : MonoBehaviour
{
    public SO_Tower towerData { get; private set; }

    private TowerBehaviour towerBehavoiur;

    public TowerType towerType;

    public int totalCost = 0;

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
        totalCost += towerData.Cost;

        towerBehavoiur = GetComponent<TowerBehaviour>();
        towerBehavoiur.SetUpTower(towerData.AttackStats, towerType);
    }

    public void ClearTower()
    {
        transform.parent.GetComponent<TowerSlot>().ClearTower();

        if (towerType == TowerType.SUPPORT)
            GetComponent<SupportTower>().ClearBuffs();

        ResourcesManager.I.AddCoins(totalCost / 2);

        Destroy(gameObject);
        return;
    }

    public void AddToTotalCost(int value)
    {
        totalCost += value;
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

    public virtual void PernamentAddStats(AttackStats bonus)
    {
        attackStats.PernamentAddStats(bonus);
    }

    public virtual void GiveBuff(AttackStats buff)
    {
        attackStats.AddStats(buff, towerType == TowerType.ATTACK_R);
    }

    public virtual void RemoveBuff(AttackStats buff)
    {
        attackStats.ReduceStats(buff, towerType == TowerType.ATTACK_R);
    }
}