using UnityEngine;

// The sound effects controller is responsible for the sound effects and their volume
public class SoundEffectsController : MonoBehaviour
{
    // The volume of sound effects
    [SerializeField] private float volume;
    // A bool tracking whether sound effects can be played
    [SerializeField] private bool soundPlaying;
    // A menu controller to change the speaker image
    private MenuManager menuManager;
    // The sound effects volume controller to disable the scrollbar when sound effects
    // are muted
    [SerializeField] private SoundEffectsVolumeController soundEffectsVolumeController;

    // Initializes the sound effects and their volume depending on the player prefs
    public void Initialize()
    {
        menuManager = FindObjectOfType<MenuManager>();

        SetSoundFromPlayerPrefs();
        SetVolumeFromPlayerPrefs();
    }

    // Returns the volume of the sound effects
    public float GetVolume()
    {
        return volume;
    }

    // Sets the volume value of sound effects to the defined value
    public void SetVolume(float volume)
    {
        this.volume = volume;

        // Changes the volume preference in the player prefs
        PlayerPrefs.SetFloat(PlayerPrefsKeys.soundEffectsVolume, volume);
        PlayerPrefs.Save();
    }

    // Sets the volume value of sound effects according to the saved player prefs
    void SetVolumeFromPlayerPrefs()
    {
        volume = PlayerPrefs.GetFloat(PlayerPrefsKeys.soundEffectsVolume);
    }

    // Toggles sound effects
    public void ToggleSound()
    {
        SetSoundPlaying(!soundPlaying);
    }

    // Sets whether a sound effect is playing based on the given value
    private void SetSoundPlaying(bool soundPlaying)
    {
        this.soundPlaying = soundPlaying;

        // Changes the speaker icon
        menuManager.SetSoundEffectsPlaying(soundPlaying);

        // Changes the volume scrollbar for the sound effects
        ChangeVolumeScrollbar();

        // Changes the sound effects preference in the player prefs
        PlayerPrefs.SetInt(PlayerPrefsKeys.soundEffectsPlaying, soundPlaying ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Sets sound effects according to the saved player prefs
    void SetSoundFromPlayerPrefs()
    {
        soundPlaying = PlayerPrefs.GetInt(PlayerPrefsKeys.soundEffectsPlaying) == 1 ? true : false;

        // Changes the speaker icon
        menuManager.SetSoundEffectsPlaying(soundPlaying);

        // Changes the volume scrollbar for the sound effects
        ChangeVolumeScrollbar();
    }

    // Plays the given sound effect
    public void PlaySound(AudioSource soundSource)
    {
        if (soundPlaying)
        {
            soundSource.volume = volume;
            soundSource.Play();
        }
    }

    // Stops the given sound effect if it is playing
    public void StopSound(AudioSource soundSource)
    {
        if (soundSource.isPlaying)
            soundSource.Stop();
    }

    // Changes the scrollbar according to whether the sound effects
    // should be played or not
    private void ChangeVolumeScrollbar()
    {
        if (soundPlaying)
            soundEffectsVolumeController.EnableVolume();
        else
            soundEffectsVolumeController.DisableVolume();
    }
}
