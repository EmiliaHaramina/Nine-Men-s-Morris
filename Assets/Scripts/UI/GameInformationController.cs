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

    // Initialize the text instructions so no text is visible
    private void Start()
    {
        playerInstructions.gameObject.SetActive(true);
        playerInstructions.text = "";
        piecesLeftText.SetActive(false);
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
}
