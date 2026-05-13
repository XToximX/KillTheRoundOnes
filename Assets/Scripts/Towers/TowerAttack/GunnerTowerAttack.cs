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

        lineRenderer.enabled = false;
    }

    private void Update()
    {
        DrawDebugLines();
    }

    protected override void HandleAttack()
    {
        targetedEnemy = TargetEnemy();

        if(targetedEnemy != null)
        {
            Vector2 enemyDir = targetedEnemy.transform.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, enemyDir, enemyLayer);
            //Debug.DrawLine(transform.position, enemyDir, Color.red, 1f);

            Debug.Log(hit.collider.name);

            //lineRenderer.SetPosition(1, hit.transform.position);
            //lineRenderer.enabled = true;

            //Invoke(nameof(DisableLineRenderer), 0.05f);
        }
    }

    private void DisableLineRenderer()
    {
        lineRenderer.enabled = false;
    }
}
