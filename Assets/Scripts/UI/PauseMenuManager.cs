using UnityEngine;

// The pause menu manager manages the pause menu in the game
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

    // The animator containing animations for the pause menu
    [SerializeField] private Animator pauseMenuAnimator;

    // The game manager should be signalled to when the game is paused
    private GameManager gameManager;

    // On starting, initializes the pause menu controls and the required booleans
    // Sets the pause menu as disabled
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        gameManager = FindObjectOfType<GameManager>();

        pauseMenuControls = new PauseMenuControls();
        pauseMenuControls.Enable();
        paused = false;
        escapePressed = false;

        // The pause menu is set as active because it is required to be active
        // for the closing animation to take place, but the scale of the
        // background element is set to (0, 0, 1) so it is not visible
        pauseMenu.SetActive(true);
        pauseMenu.transform.localScale = new Vector3(0, 0, 1);
    }

    // In the update function, check whether the player pressed the escape button
    void Update()
    {
        // Saves whether the escape button is currently pressed
        bool isEscapeKeyPressed = pauseMenuControls.PauseMenu.Escape.ReadValue<float>() == 1;
        // If the escape key is pressed and it was not pressed last frame
        if (!escapePressed && isEscapeKeyPressed)
        {
            // Set that the escape key is pressed and toggle the pause menu
            escapePressed = true;
            paused = !paused;
            gameManager.SetGamePaused(paused);
            pauseMenuAnimator.SetBool("paused", paused);
        }
        // If the escape key isn't pressed, but it was pressed last frame
        else if (escapePressed && !isEscapeKeyPressed)
            // Set that the escape key isn't pressed anymore
            escapePressed = false;
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
}
