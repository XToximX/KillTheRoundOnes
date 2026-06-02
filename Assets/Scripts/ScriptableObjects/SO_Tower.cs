using UnityEngine;

[CreateAssetMenu(fileName = "SO_Tower", menuName = "ScriptableObjects/Tower")]
public class SO_Tower : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] GameObject towerPrefab;
    

    public string Name => name;
    public GameObject TowerPrefab => towerPrefab;
    public AttackStats AttackStats;
}

[System.Serializable]
public struct AttackStats
{
    public int damage;
    public float range;
    public float attackSpeed;
}
