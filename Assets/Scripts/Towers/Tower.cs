using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof(TowerAttack))]
public class Tower : MonoBehaviour
{
    private SO_Tower towerData;

    private TowerAttack towerAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpTower(SO_Tower towerData)
    {
        this.towerData = towerData;
        towerAttack = GetComponent <TowerAttack>();
        towerAttack.SetAttackStats(towerData.AttackStats);
    }

    public void ClearTower()
    {
        transform.parent.GetComponent<TowerSlot>().ClearTower();
        Destroy(gameObject);
        return;
    }
}
