using UnityEngine;

// The settings manager contains settings logic
public class SettingsManager : MonoBehaviour
{
    // Player controller
    private PlayerController playerController;
    // Background music volume controller
    private MusicVolumeController musicVolumeController;
    // Background music controller
    private MusicController musicController;
    // Sound effects volume controller
    private SoundEffectsVolumeController soundEffectsVolumeController;
    // Sound effects controller
    private SoundEffectsController soundEffectsController;

    // Initializes the player prefs and the sound and volume for the background
    // music and sound effects
    void Start()
    {
        // Finding all required controllers
        playerController = FindObjectOfType<PlayerController>();
        musicVolumeController = FindObjectOfType<MusicVolumeController>();
        musicController = FindObjectOfType<MusicController>();
        soundEffectsVolumeController = FindObjectOfType<SoundEffectsVolumeController>();
        soundEffectsController = FindObjectOfType<SoundEffectsController>();

        // Initializes player prefs
        PlayerPrefsKeys.Initialize();

        // Initializes the players
        playerController.Initialize();

        // Initializes background music volume
        musicVolumeController.Initialize();
        // Initializes background music
        musicController.Initialize();
        // Initializes sound effects volume
        soundEffectsVolumeController.Initialize();
        // Initializes sound effects
        soundEffectsController.Initialize();
    }
}
