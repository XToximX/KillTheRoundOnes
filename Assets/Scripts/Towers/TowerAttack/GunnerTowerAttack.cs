using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GunnerTowerAttack : TowerAttack
{
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private int penetration = 1;
    private float dmgFalloff = 0.4f;

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
            Vector3 shootDir = enemyDir.normalized * attackStats.range;
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, shootDir, attackStats.range, enemyLayer);

            if(hits.Count() > 0)
            {
                float currentDmg = attackStats.damage;
                int min = penetration < (hits.Count() - 1) ? penetration : (hits.Count() - 1);
                var curPen = penetration;

                for(int i = 0; i <= min; i++)
                {
                    hits[i].collider.GetComponent<Enemy>().TakeDamage(currentDmg);
                    currentDmg = Mathf.CeilToInt(currentDmg *  (1f - dmgFalloff));

                    curPen--;
                }

                lineRenderer.enabled = true;

                if(curPen >= 0)
                {
                    lineRenderer.SetPosition(1, transform.position + shootDir);
                }
                else
                    lineRenderer.SetPosition(1, hits[min].point);


                Invoke(nameof(DisableLineRenderer), 0.05f);
            }
        }       
    }

    public void SetDamageFallof(float value)
    {
        dmgFalloff = value;
    }

    public void SetPenetration(int value)
    {
        penetration = value;
    }

    private void DisableLineRenderer()
    {
        lineRenderer.enabled = false;
    }
}
