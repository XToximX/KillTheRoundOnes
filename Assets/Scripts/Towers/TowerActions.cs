using UnityEngine;
using System.Collections.Generic;

public class TowerActions : MonoBehaviour
{
    [SerializeField] private Transform towerActionWheel;
    private Tower currentTower;

    private void Start()
    {
        InputController.I.OnEmptyClick += HideTowerActionWheel;
        InputController.I.OnInteracted += HideTowerActionWheel;

    }

    public void OpenTowerActionWheel(Tower tower)
    {
        towerActionWheel.gameObject.SetActive(true);
        towerActionWheel.position = Camera.main.WorldToScreenPoint(tower.transform.position);

        currentTower= tower;
    }

    public void ClearTower()
    {
        currentTower.ClearTower();
        currentTower.transform.parent.GetComponent<TowerSlot>().ClearTower();
        HideTowerActionWheel();
    }

    public void HideTowerActionWheel()
    {
        currentTower = null;
        towerActionWheel.gameObject.SetActive(false);
    }

    
}
