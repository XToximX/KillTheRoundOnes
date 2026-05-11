using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public static InputController I;

    public InputSystem_Actions input;

    public event Action OnEmptyClick;

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
    }


    private void OnLeftClick(InputAction.CallbackContext callback)
    {
        Vector2 mousePos = input.Gameplay.MousePos.ReadValue<Vector2>();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null)
        {
            IInteractable interactable = hit.collider?.GetComponent<IInteractable>();

            interactable.OnLeftClick();
        }
        else
            OnEmptyClick?.Invoke();
    }
}
