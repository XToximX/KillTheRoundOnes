using UnityEngine;

[CreateAssetMenu(fileName = "SO_Tower", menuName = "ScriptableObjects/Tower")]
public class SO_Tower : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] Color color = Color.white;

    public string Name => name;
    public Color Color => color;
}
