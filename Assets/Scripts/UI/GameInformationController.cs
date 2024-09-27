using TMPro;
using UnityEngine;

// The game information tells the player information about
// the ongoing game
public class GameInformationController : MonoBehaviour
{
    // The text object that writes instructions for the player
    [SerializeField] private TMP_Text playerInstructions;

    // Initialize the text instructions so no text is visible
    private void Start()
    {
        playerInstructions.gameObject.SetActive(true);
        playerInstructions.text = "";
    }

    // Changes the text to tell the current player they should place
    // their piece on the board
    public void SetPlacingText(string currentPlayerName)
    {
        playerInstructions.text = currentPlayerName + ", place a piece";
    }
}
