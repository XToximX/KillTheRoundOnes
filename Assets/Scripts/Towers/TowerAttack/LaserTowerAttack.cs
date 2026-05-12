using UnityEngine;

public class LaserTowerAttack : TowerAttack
{
    private LineRenderer lineRenderer;

    protected override void Start()
    {
        base.Start();

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        lineRenderer.positionCount = 2;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);

        InvokeRepeating(nameof(HandleAttack), 0f, attackStats.attackSpeed);
    }

    private void Update()
    {
        DrawDebugLines();

        if (targetedEnemy)
            lineRenderer.SetPosition(1, targetedEnemy.transform.position);
        else
            lineRenderer.SetPosition(1, transform.position);
    }

    public override void HandleAttack()
    {
        TargetEnemy();

        if(targetedEnemy)
            targetedEnemy.TakeDamage(attackStats.damage);
    }
}
