using UnityEngine;

// The music controller is responsible for the background music and its volume
[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    // The source of the background music
    [SerializeField] private AudioSource soundSource;
    // The volume of the background music
    [SerializeField] private float volume;
    // A bool tracking whether the background music is currently being played
    [SerializeField] private bool soundPlaying;
    // A menu controller to change the speaker image
    private MenuManager menuManager;
    // The music volume controller to disable the scrollbar when background
    // music is muted
    [SerializeField] private MusicVolumeController musicVolumeController;

    // Initializes the background music and its volume depending on the player prefs
    public void Initialize()
    {
        menuManager = FindObjectOfType<MenuManager>();

        SetSoundFromPlayerPrefs();
        SetVolumeFromPlayerPrefs();
    }

    // Returns the volume of the background music
    public float GetVolume()
    {
        return volume;
    }

    // Sets the volume value of the background music to the defined value
    public void SetVolume(float volume)
    {
        this.volume = volume;
        soundSource.volume = volume;

        // Changes the volume preference in the player prefs
        PlayerPrefs.SetFloat(PlayerPrefsKeys.musicVolume, volume);
        PlayerPrefs.Save();
    }

    // Sets the volume value of the background music according to the saved player prefs
    void SetVolumeFromPlayerPrefs()
    {
        volume = PlayerPrefs.GetFloat(PlayerPrefsKeys.musicVolume);
        soundSource.volume = volume;
    }

    // Toggles the background music
    public void ToggleSound()
    {
        if (soundPlaying)
            StopSound();
        else
            PlaySound();
    }

    // Sets the background music according to the saved player prefs
    void SetSoundFromPlayerPrefs()
    {
        bool soundPlaying = PlayerPrefs.GetInt(PlayerPrefsKeys.musicPlaying) == 1 ? true : false;
        if (soundPlaying)
            PlaySound();
        else
            StopSound();
    }
    
    // Plays the background music
    private void PlaySound()
    {
        soundPlaying = true;
        soundSource.Play();

        // Changes the speaker icon
        menuManager.SetMusicPlaying(soundPlaying);

        // Enables the volume scrollbar for the background music and sets
        // its volume to the current volume
        musicVolumeController.EnableVolume();

        // Saves the required value of the background music in the player prefs
        PlayerPrefs.SetInt(PlayerPrefsKeys.musicPlaying, 1);
        PlayerPrefs.Save();
    }

    // Stops the background music
    private void StopSound()
    {
        soundPlaying = false;
        soundSource.Pause();

        // Changes the speaker icon
        menuManager.SetMusicPlaying(soundPlaying);

        // Disables the volume scrollbar for the background music and sets
        // its volume to 0
        musicVolumeController.DisableVolume();

        // Saves the required value of the background music in the player prefs
        PlayerPrefs.SetInt(PlayerPrefsKeys.musicPlaying, 0);
        PlayerPrefs.Save();
    }
}
