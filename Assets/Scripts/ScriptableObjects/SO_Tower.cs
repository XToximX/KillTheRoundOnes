using UnityEngine;

[CreateAssetMenu(fileName = "SO_Tower", menuName = "ScriptableObjects/Tower")]
public class SO_Tower : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] GameObject towerPrefab;

    [Header("Attack stats")]
    private AttackStats attackStats;
    

    public string Name => name;
    public GameObject TowerPrefab => towerPrefab;

    public AttackStats AttackStats;
}

[System.Serializable]
public struct AttackStats
{
    public float damage;
    public float range;
    public float attackSpeed;
}
