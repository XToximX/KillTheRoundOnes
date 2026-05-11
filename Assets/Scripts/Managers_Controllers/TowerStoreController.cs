using UnityEngine;

public class TowerStoreController : MonoBehaviour
{
    [SerializeField] private Transform shopUI;

    private void Start()
    {
        InputController.I.OnEmptyClick += HideShop;
    }

    public void Shop(GameObject towerSlot)
    {
        shopUI.gameObject.SetActive(true);
        shopUI.position = Camera.main.WorldToScreenPoint(towerSlot.transform.position);
    }

    public void HideShop()
    {
        shopUI.gameObject.SetActive(false);
    }
}
