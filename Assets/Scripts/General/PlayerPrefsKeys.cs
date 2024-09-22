using UnityEngine;

// The player prefs keys manages settings properties for the current
// player so they stay the same between sessions
static public class PlayerPrefsKeys
{
    // Key for the backround music
    public static string playerPrefsMusicPlaying = "musicPlaying";
    // Key for the background music volume
    public static string playerPrefsMusicVolume = "musicVolume";

    // Initializes player prefs
    static public void Initialize()
    {
        // Initializes background music
        if (!PlayerPrefs.HasKey(playerPrefsMusicPlaying))
            PlayerPrefs.SetInt(playerPrefsMusicPlaying, 1);
        // Initializes background music volume
        if (!PlayerPrefs.HasKey(playerPrefsMusicVolume))
            PlayerPrefs.SetFloat(playerPrefsMusicVolume, 1f);
    }
}
