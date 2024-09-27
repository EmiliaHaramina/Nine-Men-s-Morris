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
    // Bool representing that the script is waiting for a player
    // to play their move
    static private bool waiting;
    // The board that player can play on
    static private Board board;
    // The current phase of the game
    static private GamePhase gamePhase;
    // The id of the player who is currently playing
    static private long currentPlayerId;
    // The number of player men
    static private int player1MenInHand;
    static private int player2MenInHand;
    static private long player1MenOnBoard;
    static private long player2MenOnBoard;
    // The player manager
    static private PlayerManager playerManager;
    // The game information controller
    static private GameInformationController gameInformationController;

    // TODO: Remove later
    [SerializeField] private int ringNumber;
    [SerializeField] private int playerMen;

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

    // The previous phase the game was in
    static private GamePhase previousPhase;
    // The number of pieces that need to be removed
    static private int piecesToRemove;

    // The different game phases
    public enum GamePhase
    {
        Placing, Moving, Flying, Removing
    }

    // Starts the game
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        gameInformationController = FindObjectOfType<GameInformationController>();

        // The game is not started until player pick who starts first
        gameStarted = false;
        // The game is not paused at the start
        gamePaused = false;
        // The script is not waiting for the player at the start
        waiting = false;
        // Initializes the board
        board = FindObjectOfType<Board>();
        board.Initialize(ringNumber);
        // Sets the number of player men
        player1MenInHand = playerMen;
        player2MenInHand = playerMen;
        player1MenOnBoard = 0;
        player2MenOnBoard = 0;

        // Sets the game phase to the first phase
        gamePhase = GamePhase.Placing;

        // Initializes the game start menu
        gameStartMenu.SetActive(true);
        player1PickText.text = PlayerPrefs.GetString(PlayerPrefsKeys.player1Name);
        player2PickText.text = PlayerPrefs.GetString(PlayerPrefsKeys.player2Name);

        player1PickButton.onClick.AddListener(() => SetStartingPlayerTurn(DefaultValues.player1Id));
        player2PickButton.onClick.AddListener(() => SetStartingPlayerTurn(DefaultValues.player2Id));

        // Shows and initializes the text letting players know how many more pieces they have
        gameInformationController.ShowPiecesLeftText();
        gameInformationController.SetPlayer1PiecesLeftText(player1MenInHand);
        gameInformationController.SetPlayer2PiecesLeftText(player2MenInHand);
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
        // If the game is paused or the script is waiting for
        // the player, nothing can be played
        if (gamePaused || waiting)
            return;

        // If the game has started, perform an action based on the current phase
        // of the game
        if (gameStarted)
        {
            // If players have no more men in hand, the placing phase of
            // the game is over
            if (gamePhase == GamePhase.Placing && player1MenInHand == 0 && player2MenInHand == 0)
            {
                // The game phase moves to the moving phase
                gamePhase = GamePhase.Moving;
                // Hides the text letting players know how many more pieces
                // they have left
                gameInformationController.HidePiecesLeftText();
            }

            // Update the board to show legal moves
            board.UpdateCurrentRound(gamePhase, currentPlayerId);
            waiting = true;

            // Update the UI instructions based on the current phase of the game
            switch (gamePhase)
            {
                case GamePhase.Placing:
                    gameInformationController.SetPlacingText(GetCurrentPlayerName());
                    break;
                case GamePhase.Moving:
                    break;
                case GamePhase.Flying:
                    break;
                case GamePhase.Removing:
                    gameInformationController.SetRemovingText(GetCurrentPlayerName());
                    break;
            }
        }
    }

    // Callback function for an illegal point that has been clicked
    static public void IllegalPointClicked()
    {
        board.PlayIllegalMoveSoundEffect();
    }

    // Callback function for a legal point that has been clicked
    static public void LegalPointClicked(Point point)
    {
        // Action depending on the current game phase
        switch (gamePhase)
        {
            // If this is the placing phase
            case GamePhase.Placing:
                // Sets the current player's man on the board
                board.SetPointPlayerId(point, currentPlayerId);
                // Plays the legal move sound effect
                board.PlayLegalMoveSoundEffect();
                // Plays an animation for placing a piece on the board
                board.PlayPlaceAnimation(point, playerManager.GetPlayerColorHexValue(currentPlayerId));
                // Increment the number of men on the board and decrease the number
                // of men in hand
                if (currentPlayerId == DefaultValues.player1Id)
                {
                    player1MenOnBoard++;
                    player1MenInHand--;
                    gameInformationController.SetPlayer1PiecesLeftText(player1MenInHand);
                }
                else if (currentPlayerId == DefaultValues.player2Id)
                {
                    player2MenOnBoard++;
                    player2MenInHand--;
                    gameInformationController.SetPlayer2PiecesLeftText(player2MenInHand);
                }

                // Check if a mill was formed
                int millsFormed = board.MillNumber(point, currentPlayerId);

                // Change the phase of the game to Removing
                if (millsFormed != 0)
                {
                    previousPhase = gamePhase;
                    gamePhase = GamePhase.Removing;
                    piecesToRemove = millsFormed;
                    waiting = false;
                    return;
                }
                break;
            case GamePhase.Moving:
                break;
            case GamePhase.Flying:
                break;
            case GamePhase.Removing:
                // Removes the other player's man from the point
                board.RemovePlayerId(point);
                // Plays the legal move sound effect
                board.PlayLegalMoveSoundEffect();
                // Plays an animation for removing a piece from the board
                board.PlayRemoveAnimation(point);
                // Decrease the number of men on the board for the other player
                if (currentPlayerId == DefaultValues.player1Id)
                {
                    player2MenOnBoard--;
                }
                else if (currentPlayerId == DefaultValues.player2Id)
                {
                    player1MenOnBoard--;
                }

                // Decrease the number of pieces to remove
                piecesToRemove--;
                // If there are no more pieces to be removed, switch back to the
                // previous phase
                if (piecesToRemove == 0)
                {
                    gamePhase = previousPhase;
                    board.ClearMillSymbols();
                }
                // If there are still more pieces that need to be removed, stay in
                // the removing phase
                else
                {
                    waiting = false;
                    return;
                }

                break;
        }

        // Switches the player
        currentPlayerId = 3 - currentPlayerId;
        // The game logic can continue
        waiting = false;
    }

    // Returns the name of the current player
    public string GetCurrentPlayerName()
    {
        return playerManager.GetPlayerName(currentPlayerId);
    }

    // Sets whether the game is paused
    public void SetGamePaused(bool gamePaused)
    {
        this.gamePaused = gamePaused;
    }
}
