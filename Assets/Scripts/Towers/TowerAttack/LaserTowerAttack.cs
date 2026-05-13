using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserTowerAttack : TowerAttack
{
    [SerializeField] private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer.positionCount = 2;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
    }

    private void Update()
    {
        DrawDebugLines();

        if (targetedEnemy)
            lineRenderer.SetPosition(1, targetedEnemy.transform.position);
        else
            lineRenderer.SetPosition(1, transform.position);
    }

    protected override void HandleAttack()
    {
        if(targetedEnemy)
            targetedEnemy.TakeDamage(attackStats.damage);
    }
}
