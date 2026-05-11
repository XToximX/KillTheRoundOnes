using UnityEngine;
using System.Collections.Generic;

public class TowerSelector : MonoBehaviour
{
    [SerializeField] private Transform towerSelector;
    private TowerSlot currentTowerSlot;

    private void Start()
    {
        InputController.I.OnEmptyClick += HideShop;
        InputController.I.OnInteracted += HideShop;
    }

    public void OpenTowerSelectWheel(TowerSlot towerSlot)
    {
        towerSelector.gameObject.SetActive(true);
        towerSelector.position = Camera.main.WorldToScreenPoint(towerSlot.transform.position);

        currentTowerSlot = towerSlot;
    }

    public void PickTower(SO_Tower towerData)
    {
        currentTowerSlot.PlaceTower(towerData);
        HideShop();
    }

    public void HideShop()
    {
        currentTowerSlot = null;
        towerSelector.gameObject.SetActive(false);
    }
}
