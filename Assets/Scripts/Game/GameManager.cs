using TMPro;
using UnityEngine;
using UnityEngine.UI;

// The game manager contains general game logic
public class GameManager : MonoBehaviour
{
    // Bool representing whether the game is currently paused
    private bool gamePaused;
    // Bool representing whether the game has started
    private bool gameStarted;
    // The board that player can play on
    private Board board;
    // The current phase of the game
    private GamePhase gamePhase;
    // The id of the player who is currently playing
    private long currentPlayerId;

    // The game start menu in which players pick who plays first
    [SerializeField] private GameObject gameStartMenu;
    // Text for picking player 1
    [SerializeField] private TMP_Text player1PickText;
    // Text for picking player 2
    [SerializeField] private TMP_Text player2PickText;
    // Button for picking player 1
    [SerializeField] private Button player1PickButton;
    // Button for picking player 2
    [SerializeField] private Button player2PickButton;

    // The different game phases
    public enum GamePhase
    {
        Placing, Moving, Flying
    }

    // Starts the game
    void Start()
    {
        // The game is not started until player pick who starts first
        gameStarted = false;
        // The game is not paused at the start
        gamePaused = false;
        // Initializes the board
        board = FindObjectOfType<Board>();
        board.Initialize(3);
        // Sets the game phase to the first phase
        gamePhase = GamePhase.Placing;

        // Initializes the game start menu
        gameStartMenu.SetActive(true);
        player1PickText.text = PlayerPrefs.GetString(PlayerPrefsKeys.player1Name);
        player2PickText.text = PlayerPrefs.GetString(PlayerPrefsKeys.player2Name);

        player1PickButton.onClick.AddListener(() => SetStartingPlayerTurn(DefaultValues.player1Id));
        player2PickButton.onClick.AddListener(() => SetStartingPlayerTurn(DefaultValues.player2Id));
    }

    // Sets which player's turn it currently is, hides the game start
    // menu, and starts the game
    void SetStartingPlayerTurn(long playerId)
    {
        currentPlayerId = playerId;
        gameStartMenu.SetActive(false);
        gameStarted = true;
    }

    // Game updates
    void Update()
    {
        // If the game is paused, nothing can be played
       if (gamePaused)
            return;

       if (gameStarted)
        {

        }
    }

    // Sets whether the game is paused
    public void SetGamePaused(bool gamePaused)
    {
        this.gamePaused = gamePaused;
    }
}
