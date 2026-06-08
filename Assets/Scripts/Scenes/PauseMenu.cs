using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseCanva;
    private bool isGamePaused = false;
    public bool IsGamePaused => isGamePaused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputController.I.input.Pause.Esc.performed += EscBtnPerformed;
        pauseCanva.SetActive(false);
    }

    private void EscBtnPerformed(InputAction.CallbackContext callback)
    {
        PauseGame();
    }

    public void PauseGame()
    {
        isGamePaused = !isGamePaused;

        TimeManager.I.PauseTime();
        pauseCanva.SetActive(isGamePaused);

        if (isGamePaused)
            InputController.I.input.Gameplay.Disable();
        else
            InputController.I.input.Gameplay.Enable();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        InputController.I.input.Pause.Esc.performed -= EscBtnPerformed;
    }
}
