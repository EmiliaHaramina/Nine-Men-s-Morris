using UnityEngine;

// The settings manager contains settings logic
public class SettingsManager : MonoBehaviour
{
    // Background music controller
    private MusicController musicController;
    // Background music volume controller
    private MusicVolumeController musicVolumeController;
    // Sound effects controller
    private SoundEffectsController soundEffectsController;
    // Sound effects volume controller
    private SoundEffectsVolumeController soundEffectsVolumeController;

    // Initializes the player prefs and the sound and volume for the background
    // music and sound effects
    void Start()
    {
        // Finding all required controllers
        musicController = FindObjectOfType<MusicController>();
        musicVolumeController = FindObjectOfType<MusicVolumeController>();
        soundEffectsController = FindObjectOfType<SoundEffectsController>();
        soundEffectsVolumeController = FindObjectOfType<SoundEffectsVolumeController>();

        // Initializes player prefs
        PlayerPrefsKeys.Initialize();

        // Initializes background music
        musicController.Initialize();
        // Initializes background music volume
        musicVolumeController.Initialize();
        // Initializes sound effects
        soundEffectsController.Initialize();
        // Initializes sound effects volume
        soundEffectsVolumeController.Initialize();
    }
}
