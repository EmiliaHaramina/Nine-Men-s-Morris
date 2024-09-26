using UnityEngine;
using UnityEngine.UI;

// The sound manager is responsible for the sound UI
public class SoundManager : MonoBehaviour
{
    // Parameters required for changing the music and sound effects
    [SerializeField] private Sprite speakerOnSprite;
    [SerializeField] private Sprite speakerOffSprite;
    [SerializeField] private Image musicSpeakerImage;
    [SerializeField] private Image soundEffectsSpeakerImage;
    private bool musicPlaying;
    private bool soundEffectsPlaying;

    // Sets the background music to play
    public void SetMusicPlaying(bool musicPlaying)
    {
        this.musicPlaying = musicPlaying;
        ChangeMusicSpeakerImage();
    }

    // Toggles the background music
    public void ToggleMusicPlaying()
    {
        musicPlaying = !musicPlaying;
        ChangeMusicSpeakerImage();
    }

    // Sets the sound effects to play
    public void SetSoundEffectsPlaying(bool soundEffectsPlaying)
    {
        this.soundEffectsPlaying = soundEffectsPlaying;
        ChangeSoundEffectsSpeakerImage();
    }

    // Toggles the sound effects
    public void ToggleSoundEffectsPlaying()
    {
        soundEffectsPlaying = !soundEffectsPlaying;
        ChangeSoundEffectsSpeakerImage();
    }

    // Sets the background music speaker sprite to be on
    public void SetMusicSpeakerOnSprite()
    {
        musicSpeakerImage.sprite = speakerOnSprite;
    }

    // Sets the background music speaker sprite to be off
    public void SetMusicSpeakerOffSprite()
    {
        musicSpeakerImage.sprite = speakerOffSprite;
    }

    // Sets the sound effects speaker sprite to be on
    public void SetSoundEffectsSpeakerOnSprite()
    {
        soundEffectsSpeakerImage.sprite = speakerOnSprite;
    }

    // Sets the sound effects speaker sprite to be off
    public void SetSoundEffectsSpeakerOffSprite()
    {
        soundEffectsSpeakerImage.sprite = speakerOffSprite;
    }

    // Changes the background music speaker image according to whether
    // the background music is currently playing
    private void ChangeMusicSpeakerImage()
    {
        if (musicPlaying)
            SetMusicSpeakerOnSprite();
        else
            SetMusicSpeakerOffSprite();
    }

    // Changes the sound effects speaker image according to whether
    // the background sound effects is currently playing
    private void ChangeSoundEffectsSpeakerImage()
    {
        if (soundEffectsPlaying)
            SetSoundEffectsSpeakerOnSprite();
        else
            SetSoundEffectsSpeakerOffSprite();
    }
}
