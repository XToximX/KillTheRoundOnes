using UnityEngine;
using UnityEngine.UI;

public class TowerActions : MonoBehaviour
{
    [SerializeField] private Transform towerActionWheel;
    [SerializeField] private GameObject targetingMenu;
    [SerializeField] private GameObject towerRangeIndicator;

    [Header("Level indicator")]
    [SerializeField] Sprite emptyLvlSprite;
    [SerializeField] Sprite fullLvlSprite;
    [SerializeField] Image[] lvlIndicators;


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

        ShowTowerLevel();

        if (towerType != TowerType.ATTACK_R)
            targetingMenu.SetActive(false);

        if(towerType != TowerType.ATTACK_M)
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
        TowerUpgrade towerUpgrade = currentTower.GetComponent<TowerUpgrade>();

        if (!towerUpgrade.isMaxLvl())
        {
            int upgradeCost = towerUpgrade.LvlCosts[towerUpgrade.CurrentLvl];

            if(ResourcesManager.I.Buy(upgradeCost, towerUpgrade.LvlUp))
            {
                ShowTowerLevel();

                if(towerType != TowerType.ATTACK_M)
                    ShowTowerRange();

                currentTower.AddToTotalCost(upgradeCost);
            }
        }
        //HideTowerActionWheel();
    }

    private void ShowTowerLevel()
    {
        int towerLvl = currentTower.GetComponent<TowerUpgrade>().CurrentLvl;
        int count = 0;
        
        foreach(Image image in lvlIndicators)
        {
            if (count < towerLvl)
                image.sprite = fullLvlSprite;
            else
                image.sprite = emptyLvlSprite;
            
            count++;
        }
    }
}