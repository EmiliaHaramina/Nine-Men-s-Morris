using UnityEngine;

// The game manager contains general game logic
public class GameManager : MonoBehaviour
{
    // Bool representing whether the game is currently paused
    private bool gamePaused;
    // The board that player can play on
    private Board board;

    // Sets that the game isn't paused at the start
    void Start()
    {
        gamePaused = false;
        board = new();
        board.Initialize(3);
    }

    // Sets whether the game is paused
    public void SetGamePaused(bool gamePaused)
    {
        this.gamePaused = gamePaused;
    }
}
