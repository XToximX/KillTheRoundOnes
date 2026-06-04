using UnityEngine;

public class BladeContact : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
        }
    }
}
