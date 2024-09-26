using UnityEngine;

// The menu manager manages menu functions
abstract public class MenuManager : MonoBehaviour
{
    protected internal SoundManager soundManager;

    // Sets whether the background music is currently playing
    // according to the given bool
    public void SetMusicPlaying(bool musicPlaying)
    {
        soundManager.SetMusicPlaying(musicPlaying);
    }

    // Toggles the background music
    public void ToggleMusic()
    {
        soundManager.ToggleMusicPlaying();
    }

    // Sets whether the sound effects are currently playing
    // according to the given bool
    public void SetSoundEffectsPlaying(bool soundEffectsPlaying)
    {
        soundManager.SetSoundEffectsPlaying(soundEffectsPlaying);
    }

    // Toggles the sound effects
    public void ToggleSoundEffects()
    {
        soundManager.ToggleSoundEffectsPlaying();
    }
}
