using UnityEngine;

// The game manager contains general game logic
public class GameManager : MonoBehaviour
{
    // Bool representing whether the game is currently paused
    private bool gamePaused;

    // Sets that the game isn't paused at the start
    void Start()
    {
        gamePaused = false;
    }

    // Sets whether the game is paused
    public void SetGamePaused(bool gamePaused)
    {
        this.gamePaused = gamePaused;
    }
}
