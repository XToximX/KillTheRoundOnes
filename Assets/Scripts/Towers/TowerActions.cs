using UnityEngine;
using System.Collections.Generic;
using System;

public class TowerActions : MonoBehaviour
{
    [SerializeField] private Transform towerActionWheel;
    [SerializeField] private GameObject targetingMenu;
    [SerializeField] private GameObject towerRangeIndicator;

    private Tower currentTower;
    private TowerType towerType;

    private void Start()
    {
        InputController.I.OnEmptyClick += HideTowerActionWheel;
        InputController.I.OnInteracted += HideTowerActionWheel;
        targetingMenu.SetActive(false);
    }

    public void OpenTowerActionWheel(Tower tower)
    {
        currentTower = tower;
        towerType = tower.towerType;

        towerActionWheel.gameObject.SetActive(true);
        towerActionWheel.position = Camera.main.WorldToScreenPoint(tower.transform.position);

        if (towerType != TowerType.ATTACK_R)
            targetingMenu.SetActive(false);


        ShowTowerRange();
    }

    public void ClearTower()
    {
        currentTower.ClearTower();
        HideTowerActionWheel();
    }

    public void HideTowerActionWheel()
    {
        targetingMenu.SetActive(false);
        towerActionWheel.gameObject.SetActive(false);
        towerRangeIndicator.SetActive(false);

        currentTower = null;
    }

    public void ShowTargetingMenu()
    {
        targetingMenu.SetActive(true);
    }

    private void ShowTowerRange()
    {
        float towerRange = currentTower.GetComponent<TowerBehaviour>().GetAttackStats().range;
        towerRangeIndicator.transform.position = currentTower.transform.position;
        towerRangeIndicator.transform.localScale = new Vector3(towerRange * 2, towerRange * 2, 1f);
        towerRangeIndicator.SetActive(true);
    }

    public void ChangeTargetingMode(string targetingModeName)
    {
        TowerAttack towerAttackScript = currentTower.GetComponent<TowerAttack>();

        switch(targetingModeName)
        {
            case "FIRST":
                towerAttackScript.ChangeTargetingMode(TargetingMode.FIRST);
                break;

            case "LAST":
                towerAttackScript.ChangeTargetingMode(TargetingMode.LAST);
                break;

            case "FURTHEST":
                towerAttackScript.ChangeTargetingMode(TargetingMode.FURTHEST);
                break;

            case "CLOSEST":
                towerAttackScript.ChangeTargetingMode(TargetingMode.CLOSEST);
                break;

            case "STRONGEST":
                towerAttackScript.ChangeTargetingMode(TargetingMode.STRONGEST);
                break;

            case "WEAKEST":
                towerAttackScript.ChangeTargetingMode(TargetingMode.WEAKEST);
                break;
        }

        HideTowerActionWheel();
    }

    public void LvlUpTower()
    {
        currentTower.GetComponent<TowerUpgrade>().LvlUp();

        HideTowerActionWheel();
    }
}