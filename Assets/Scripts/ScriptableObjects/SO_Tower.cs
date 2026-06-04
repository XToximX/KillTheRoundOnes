using UnityEngine;

[CreateAssetMenu(fileName = "SO_Tower", menuName = "ScriptableObjects/Tower")]
public class SO_Tower : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] GameObject towerPrefab;
    

    public string Name => name;
    public TowerType towerType;

    public GameObject TowerPrefab => towerPrefab;
    public AttackStats AttackStats;
}

[System.Serializable]
public struct AttackStats
{
    public int damage;
    public float range;
    public float attackSpeed;

    public void AddStats(AttackStats bonus, bool isRanged)
    {
        damage += bonus.damage;
        attackSpeed += bonus.attackSpeed;
        if (isRanged)
            range += bonus.range;
    }

    public void ReduceStats(AttackStats reduction, bool isRanged)
    {
        if(damage - reduction.damage > 0)
            damage -= reduction.damage;
        else
            damage = 0;

        if (attackSpeed - reduction.attackSpeed > 0)
            attackSpeed -= reduction.attackSpeed;
        else
            attackSpeed = 0;

        if(isRanged)
        {
            if (range - reduction.range > 0)
                range -= reduction.range;
            else 
                range = 0;
        }
    }
}
