using UnityEngine;

// The player prefs keys manages settings properties for the current
// player so they stay the same between sessions
static public class PlayerPrefsKeys
{
    // Key for the backround music
    public static string musicPlaying = "musicPlaying";
    // Key for the background music volume
    public static string musicVolume = "musicVolume";
    // Key for the sound effects
    public static string soundEffectsPlaying = "soundEffectsPlaying";
    // Key for the sound effects volume
    public static string soundEffectsVolume = "soundEffectsVolume";

    //Key for the name of player 1
    public static string player1Name = "player1Name";
    //Key for the color of player 1
    public static string player1Color = "player1Color";
    //Key for the name of player 2
    public static string player2Name = "player2Name";
    //Key for the color of player 2
    public static string player2Color = "player2Color";

    // Initializes player prefs
    static public void Initialize()
    {
        // Initializes background music
        if (!PlayerPrefs.HasKey(musicPlaying))
            PlayerPrefs.SetInt(musicPlaying, DefaultValues.musicPlaying);
        // Initializes background music volume
        if (!PlayerPrefs.HasKey(musicVolume))
            PlayerPrefs.SetFloat(musicVolume, DefaultValues.musicVolume);
        // Initializes sound effects
        if (!PlayerPrefs.HasKey(soundEffectsPlaying))
            PlayerPrefs.SetInt(soundEffectsPlaying, DefaultValues.soundEffectsPlaying);
        // Initializes sound effects volume
        if (!PlayerPrefs.HasKey(soundEffectsVolume))
            PlayerPrefs.SetFloat(soundEffectsVolume, DefaultValues.soundEffectsVolume);

        // Initializes the name of player 1
        if (!PlayerPrefs.HasKey(player1Name))
            PlayerPrefs.SetString(player1Name, "Player 1");
        // Initializes the color of player 1
        if (!PlayerPrefs.HasKey(player1Color))
            PlayerPrefs.SetString(player1Color, "#000000");
        // Initializes the name of player 2
        if (!PlayerPrefs.HasKey(player2Name))
            PlayerPrefs.SetString(player2Name, "Player 2");
        // Initializes the color of player 2
        if (!PlayerPrefs.HasKey(player2Color))
            PlayerPrefs.SetString(player2Color, "#FFFFFF");

        // Saves the player prefs
        PlayerPrefs.Save();
    }
}
