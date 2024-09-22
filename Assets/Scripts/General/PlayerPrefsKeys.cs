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
    //public static string playerPre

    // Initializes player prefs
    static public void Initialize()
    {
        // Initializes background music
        if (!PlayerPrefs.HasKey(musicPlaying))
            PlayerPrefs.SetInt(musicPlaying, 1);
        // Initializes background music volume
        if (!PlayerPrefs.HasKey(musicVolume))
            PlayerPrefs.SetFloat(musicVolume, 1f);
    }
}
