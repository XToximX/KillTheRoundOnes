using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GunnerTowerAttack : TowerAttack
{
    [SerializeField] private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer.positionCount = 2;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);

        lineRenderer.enabled = true;
    }

    private void Update()
    {
        DrawDebugLines();
    }

    protected override void HandleAttack()
    {
        if(targetedEnemy != null)
        {
            Vector2 enemyDir = targetedEnemy.transform.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, enemyDir, attackStats.range, enemyLayer);

            if (hit.collider)
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(attackStats.damage);

                lineRenderer.enabled = true;
                lineRenderer.SetPosition(1, hit.point);

                Invoke(nameof(DisableLineRenderer), 0.05f);
            }
        }
    }

    private void DisableLineRenderer()
    {
        lineRenderer.enabled = false;
    }
}
