using UnityEngine;

public class TowerSlot : MonoBehaviour, IInteractable
{
    TowerStoreController towerStoreController;

    private void Start()
    {
        towerStoreController = GameObject.Find("TowerStoreController").GetComponent<TowerStoreController>();
    }

    public void OnLeftClick()
    {
        towerStoreController.Shop(gameObject);
    }
}
