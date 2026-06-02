using UnityEngine;

public class BladeTowerAttack : TowerAttack
{
    [SerializeField] GameObject blade;

    public override void SetAttackStats(AttackStats attackStats)
    {
        this.attackStats = attackStats;
        blade.GetComponent<BladeContact>().damage = attackStats.damage;

        enemyLayer = LayerMask.GetMask("Enemy");

        blade.transform.localScale = new Vector3 (0.3f, attackStats.range * 2, 1);
    }

    // Update is called once per frame
    void Update()
    {
        float rotationSpeed = 360f / attackStats.attackSpeed;
        blade.transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
    }

    protected override void HandleAttack()
    {
        return;
    }
}
