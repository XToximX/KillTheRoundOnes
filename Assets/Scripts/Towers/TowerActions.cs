using UnityEngine;
using System.Collections.Generic;
using System;

public class TowerActions : MonoBehaviour
{
    [SerializeField] private Transform towerActionWheel;
    [SerializeField] private GameObject targetingMenu;
    private Tower currentTower;

    private void Start()
    {
        InputController.I.OnEmptyClick += HideTowerActionWheel;
        InputController.I.OnInteracted += HideTowerActionWheel;
        targetingMenu.SetActive(false);
    }

    public void OpenTowerActionWheel(Tower tower)
    {
        towerActionWheel.gameObject.SetActive(true);
        towerActionWheel.position = Camera.main.WorldToScreenPoint(tower.transform.position);

        currentTower = tower;
    }

    public void ClearTower()
    {
        currentTower.ClearTower();
        HideTowerActionWheel();
    }

    public void HideTowerActionWheel()
    {
        currentTower = null;
        targetingMenu.SetActive(false);
        towerActionWheel.gameObject.SetActive(false);
    }

    public void ShowTargetingMenu()
    {
        targetingMenu.SetActive(true);
    }

    public void ChangeTargetingMode(string targetingModeName)
    {
        switch(targetingModeName)
        {
            case "FIRST":
                currentTower.ChangeTargetingMode(TargetingMode.FIRST);
                break;

            case "LAST":
                currentTower.ChangeTargetingMode(TargetingMode.LAST);
                break;

            case "FURTHEST":
                currentTower.ChangeTargetingMode(TargetingMode.FURTHEST);
                break;

            case "CLOSEST":
                currentTower.ChangeTargetingMode(TargetingMode.CLOSEST);
                break;

            case "STRONGEST":
                currentTower.ChangeTargetingMode(TargetingMode.STRONGEST);
                break;

            case "WEAKEST":
                currentTower.ChangeTargetingMode(TargetingMode.WEAKEST);
                break;
            default:
                currentTower.ChangeTargetingMode(TargetingMode.FIRST);
                break;
        }

        HideTowerActionWheel();
    }
}