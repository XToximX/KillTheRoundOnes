using UnityEngine;

public class BladeTowerAttack : TowerAttack
{
    [SerializeField] Transform bladesCenter;

    public override void SetUpTower(AttackStats attackStats, TowerType towerType)
    {
        this.attackStats = attackStats;
        this.towerType = towerType;

        enemyLayer = LayerMask.GetMask("Enemy");

        SetBlades();
    }

    // Update is called once per frame
    void Update()
    {
        float rotationSpeed = 360f / (1f / attackStats.attackSpeed);
        bladesCenter.transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
    }

    public void SetBlades()
    {
        foreach(Transform blade in bladesCenter)
        {
            blade.localScale = new Vector3(0.3f, attackStats.range * 2, 1);
        }
    }

    protected override void HandleAttack()
    {
        return;
    }
}
