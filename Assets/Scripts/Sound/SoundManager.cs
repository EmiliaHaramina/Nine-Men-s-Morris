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

    public void SetMusicPlaying(bool musicPlaying)
    {
        this.musicPlaying = musicPlaying;
        ChangeMusicSpeakerImage();
    }

    public void ToggleMusicPlaying()
    {
        musicPlaying = !musicPlaying;
        ChangeMusicSpeakerImage();
    }

    public void SetSoundEffectsPlaying(bool soundEffectsPlaying)
    {
        this.soundEffectsPlaying = soundEffectsPlaying;
        ChangeSoundEffectsSpeakerImage();
    }

    public void ToggleSoundEffectsPlaying()
    {
        soundEffectsPlaying = !soundEffectsPlaying;
        ChangeSoundEffectsSpeakerImage();
    }

    public void SetMusicSpeakerOnSprite()
    {
        musicSpeakerImage.sprite = speakerOnSprite;
    }

    public void SetMusicSpeakerOffSprite()
    {
        musicSpeakerImage.sprite = speakerOffSprite;
    }

    public void SetSoundEffectsSpeakerOnSprite()
    {
        soundEffectsSpeakerImage.sprite = speakerOnSprite;
    }

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
