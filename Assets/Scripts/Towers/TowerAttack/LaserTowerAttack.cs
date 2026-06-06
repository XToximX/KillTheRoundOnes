using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserTowerAttack : TowerAttack
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private ParticleSystem particle;
    private float baseEmissionRate = 25f;

    private bool wasTargeting;


    private bool isDamageRampingOn = false;
    Enemy lastTarget = null;
    float dmgRampMult = 1f;
    float dmgRampTime = 0f;

    private void Start()
    {
        lineRenderer.positionCount = 2;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);

        particle.Stop();
    }

    private void Update()
    {
        DrawDebugLines();

        if (targetedEnemy)
        {
            lineRenderer.SetPosition(1, targetedEnemy.transform.position);
            particle.transform.position = targetedEnemy.transform.position;

            if(!wasTargeting)
            {
                particle.Play();
            }

            wasTargeting = true;
        }
        else
        {
            lineRenderer.SetPosition(1, transform.position);

            if(wasTargeting)
                particle.Stop();

            wasTargeting = false;

        }
    }

    protected override void HandleAttack()
    {
        if (targetedEnemy)
        {
            //Debug.Log($"Current damage: {Mathf.CeilToInt(attackStats.damage * dmgRampMult)}");

            if (isDamageRampingOn)
            {
                var emission = particle.emission;

                if (targetedEnemy == lastTarget)
                {
                    if (dmgRampTime < 0.5f)
                        dmgRampMult = 1f;
                    else if (dmgRampTime < 1f)
                        dmgRampMult = 1.5f;
                    else
                        dmgRampMult = 2f;

                    dmgRampTime += (1f / attackStats.attackSpeed);
                    emission.rateOverTimeMultiplier = baseEmissionRate * dmgRampMult * 5f;
                }
                else
                {
                    dmgRampMult = 1f;
                    dmgRampTime = 0f;

                    emission.rateOverTime = baseEmissionRate;
                }
                
                lastTarget = targetedEnemy;
            }

            //Debug.Log($"{dmgRampTime}, {dmgRampMult}");
            targetedEnemy.TakeDamage(Mathf.CeilToInt(attackStats.damage * dmgRampMult));
        }
    }

    public void EnableDamageRamping()
    {
        isDamageRampingOn = true;
    }
}
