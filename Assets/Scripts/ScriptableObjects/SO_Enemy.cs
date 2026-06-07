using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemy")]
public class SO_Enemy : ScriptableObject
{
    [Header("Visual")]
    [SerializeField] new string name;
    [SerializeField] Color color;
    [SerializeField] AudioClip deathClip;

    [Header("Stats")]
    [SerializeField] float speed;
    [SerializeField] int hp;
    [SerializeField] int dmg;
    [SerializeField] float size = 1f;

    [Header("Wave info")]
    [SerializeField] int waveCost;
    [SerializeField] int minWave;

    [Range(1f, 100f)]
    [SerializeField] private float spawnWeight = 1;
    [SerializeField] private AnimationCurve weightCurve;


    public string Name => name;
    public Color Color => color;
    public AudioClip DeathClip => deathClip;
    public float Speed => speed;
    public int HP => hp;
    public int Dmg => dmg;
    public float Size => size;
    public int WaveCost => waveCost;
    public int MinWave => minWave;
    public float SpawnWeight => spawnWeight * weightCurve.Evaluate(WavesManager.I.CurrentWave);
}
