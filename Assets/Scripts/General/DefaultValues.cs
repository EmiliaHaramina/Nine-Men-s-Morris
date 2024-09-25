using UnityEngine;

// The default value class keeps track of default values throughout
// the game
static public class DefaultValues
{
    // Default values for background music and sound effects
    static public int musicPlaying = 1;
    static public float musicVolume = 1f;
    static public int soundEffectsPlaying = 1;
    static public float soundEffectsVolume = 1f;

    // Default values for player properties
    static public long player1Id = 1;
    static public string player1Name = "Player 1";
    static public string player1ColorHexValue = "#000000";
    static public long player2Id = 2;
    static public string player2Name = "Player 2";
    static public string player2ColorHexValue = "#FFFFFF";

    // Default values for color choices
    static public string colorAvailableHexValue = "#414141";
    static public string colorPickedHexValue = "#6DFF64";
    static public string colorUnavailableHexValue = "#FF0000";
    static public float panelColorTransparency = 100f;
}
