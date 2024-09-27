using TMPro;
using UnityEngine;

// The game information tells the player information about
// the ongoing game
public class GameInformationController : MonoBehaviour
{
    // The text object that writes instructions for the player
    [SerializeField] private TMP_Text playerInstructions;

    // Text objects that count the number of pieces left from each
    // player
    [SerializeField] private GameObject piecesLeftText;
    [SerializeField] private TMP_Text player1PiecesLeft;
    [SerializeField] private TMP_Text player2PiecesLeft;

    // Text objects that show the winner of the game
    [SerializeField] private GameObject winnerMenu;
    [SerializeField] private TMP_Text winnerText;

    // The victory sound effect source
    [SerializeField] private SoundEffectSource victorySoundEffectSource;

    // Initialize the text instructions so no text is visible
    private void Start()
    {
        playerInstructions.gameObject.SetActive(true);
        playerInstructions.text = "";
        piecesLeftText.SetActive(false);

        // The pause menu is set as active because it is required to be active
        // for the closing animation to take place, but the scale of the
        // background element is set to (0, 0, 1) so it is not visible
        winnerMenu.SetActive(true);
        //winnerMenu.transform.localScale = new Vector3(0, 0, 1);
    }

    // Changes the text to tell the current player they should place
    // their piece on the board
    public void SetPlacingText(string currentPlayerName)
    {
        playerInstructions.text = currentPlayerName + ", place a piece";
    }

    // Changes the text to tell the current player they should remove
    // one of their opponent's pieces
    public void SetRemovingText(string currentPlayerName)
    {
        playerInstructions.text = currentPlayerName + ", remove a piece";
    }

    // Changes the text to tell the current player they should pick a
    // piece to move
    public void SetMoving1Text(string currentPlayerName)
    {
        playerInstructions.text = currentPlayerName + ", pick a piece to move";
    }

    // Changes the text to tell the current player they should pick a
    // place to move
    public void SetMoving2Text(string currentPlayerName)
    {
        playerInstructions.text = currentPlayerName + ", move your piece";
    }

    // Sets the text for the number of pieces player 1 has left
    public void SetPlayer1PiecesLeftText(int piecesLeft)
    {
        string player1Name = PlayerPrefs.GetString(PlayerPrefsKeys.player1Name);
        player1PiecesLeft.text = player1Name + "\nPieces left:\n" + piecesLeft;
    }
    
    // Sets the text for the number of pieces player 2 has left
    public void SetPlayer2PiecesLeftText(int piecesLeft)
    {
        string player2Name = PlayerPrefs.GetString(PlayerPrefsKeys.player2Name);
        player2PiecesLeft.text = player2Name + "\nPieces left:\n" + piecesLeft;
    }

    // Enables the text letting players know how many pieces they have left
    public void ShowPiecesLeftText()
    {
        piecesLeftText.SetActive(true);
    }

    // Disables the text letting players know how many pieces they have left
    public void HidePiecesLeftText()
    {
        piecesLeftText.SetActive(false);
    }
    
    // Hides the instructions text
    public void HideInstructions()
    {
        playerInstructions.gameObject.SetActive(false);
    }

    // Shows the winner of the game and plays the victory sound effect
    public void ShowWinnerText(string playerName)
    {
        winnerMenu.GetComponent<Animator>().SetBool("gameEnded", true);
        winnerText.text = playerName + " WON\nTHE GAME!";
        victorySoundEffectSource.PlaySoundEffect();
    }
}
