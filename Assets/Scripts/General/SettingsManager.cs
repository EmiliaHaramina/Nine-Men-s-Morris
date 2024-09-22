using UnityEngine;

// The settings manager contains settings logic
public class SettingsManager : MonoBehaviour
{
    // Background music controller
    [SerializeField] private MusicController musicController;
    // Background music volume controller
    [SerializeField] private MusicVolumeController musicVolumeController;
    // Sound effects controller
    [SerializeField] private SoundEffectsController soundEffectsController;
    // Sound effects volume controller
    [SerializeField] private SoundEffectsVolumeController soundEffectsVolumeController;

    // Initializes the player prefs and the sound and volume for the background
    // music
    void Start()
    {
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
