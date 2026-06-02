using UnityEngine;

[CreateAssetMenu(fileName = "TowerUpgrades", menuName = "ScriptableObjects/Upgrades")]
public class SO_Upgrades : ScriptableObject
{
    public AttackStats[] values = new AttackStats[3];
}
