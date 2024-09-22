using UnityEngine;

static public class PlayerPrefsKeys
{
    public static string playerPrefsVolume = "volume";
    public static string playerPrefsMusicPlaying = "musicPlaying";

    static public void Initialize()
    {
        if (!PlayerPrefs.HasKey(playerPrefsMusicPlaying))
            PlayerPrefs.SetInt(playerPrefsMusicPlaying, 1);
        if (!PlayerPrefs.HasKey(playerPrefsVolume))
            PlayerPrefs.SetFloat(playerPrefsVolume, 1f);
    }
}
