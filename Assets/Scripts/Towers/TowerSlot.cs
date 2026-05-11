using UnityEngine;

public class TowerSlot : MonoBehaviour, IInteractable
{
    TowerSelector towerSelector;
    TowerActions towerActions;
    private TowerSpotState spotState = TowerSpotState.EMPTY;
    private Tower tower;

    private void Start()
    {
        towerSelector = FindAnyObjectByType<TowerSelector>();
        towerActions = FindAnyObjectByType<TowerActions>();
        tower = transform.GetChild(0).GetComponent<Tower>();
    }

    public void OnLeftClick()
    {
        if (spotState == TowerSpotState.EMPTY)
        {
            towerSelector.OpenTowerSelectWheel(this);
        }
        else
            towerActions.OpenTowerActionWheel(tower);
    }

    public void PlaceTower(SO_Tower towerData)
    {
        spotState = TowerSpotState.OCCUPIED;

        tower.SetUpTower(towerData);
    }

    public void ClearTower()
    {
        spotState = TowerSpotState.EMPTY;
        tower.ClearTower();
    }
}

public enum TowerSpotState
{
    EMPTY,
    OCCUPIED
}