using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    public static InputController I;

    public InputSystem_Actions input;

    public event Action OnEmptyClick;
    public event Action OnInteracted;

    private bool isMouseOverUI = false;

    private void Awake()
    {
        if(I != null && I != this )
        {
            Destroy(gameObject);
            return;
        }

        I = this;

        input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input.Gameplay.Enable();

        input.Gameplay.LClick.performed += OnLeftClick;
        input.Gameplay.RClick.performed += (callback) => OnEmptyClick?.Invoke();
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() != isMouseOverUI)
            isMouseOverUI = !isMouseOverUI;
    }

    private void OnLeftClick(InputAction.CallbackContext callback)
    {
        if (isMouseOverUI)
            return;

        Vector2 mousePos = input.Gameplay.MousePos.ReadValue<Vector2>();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if(hit.collider != null)
        {
            switch(hit.collider.gameObject.tag)
            {
                case "TowerSlot":
                    IInteractable interactable = hit.collider?.GetComponent<IInteractable>();

                    OnInteracted?.Invoke();
                    interactable.OnLeftClick();
                    break;

                case "Enemy":
                    OnInteracted?.Invoke();
                    break;

                default:
                    OnEmptyClick?.Invoke();
                    break;
            }
        }
        else
            OnEmptyClick?.Invoke();
    }
}
