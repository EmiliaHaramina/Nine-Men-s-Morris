using UnityEngine;

public class PauseMenuManager : MenuManager
{
    // The pause menu
    [SerializeField] private GameObject pauseMenu;
    // Bool checking whether the game is currently paused
    private bool paused;
    // Bool checking whether the pause key has been pressed
    private bool escapePressed;

    // The input actions for pausing the game
    private PauseMenuControls pauseMenuControls;

    private void Start()
    {
        pauseMenuControls = new PauseMenuControls();
        pauseMenuControls.Enable();
        paused = false;
        escapePressed = false;
    }

    private void Update()
    {
        bool isEscapeKeyPressed = pauseMenuControls.PauseMenu.Escape.ReadValue<float>() == 1;
        if (!escapePressed && isEscapeKeyPressed)
        {
            escapePressed = true;
            Debug.Log("Pressed");
            // TODO
        }
        else if (escapePressed && !isEscapeKeyPressed)
        {
            escapePressed = false;
            Debug.Log("Stopped pressing");
        }
    }

    // Opens the pause menu
    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    // Closes the pause menu
    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    // Returns true if the game is currently paused
    public bool IsPaused()
    {
        return paused;
    }
}
