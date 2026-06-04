using Unity.VisualScripting;
using UnityEngine;

public class BladeContact : MonoBehaviour
{
    private TowerAttack towerAttack;
    public int damage;

    private void Start()
    {
        towerAttack = transform.parent.parent.GetComponent<TowerAttack>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(towerAttack.GetAttackStats().damage);
        }
    }
}
