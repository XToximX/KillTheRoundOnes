using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemy")]
public class SO_Enemy : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] Color color;
    [SerializeField] float speed;
    [SerializeField] int hp;

    public string Name => name;
    public Color Color => color;
    public float Speed => speed;
    public int HP => hp;
}
