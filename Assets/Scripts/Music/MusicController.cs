using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private float volume;
    [SerializeField] private bool musicPlaying;

    public void Initialize()
    {
        SetVolume(PlayerPrefs.GetFloat(PlayerPrefsKeys.playerPrefsVolume));;
        SetMusic(PlayerPrefs.GetInt(PlayerPrefsKeys.playerPrefsMusicPlaying) == 1 ? true : false);
    }

    public float GetVolume()
    {
        return volume;
    }

    public void ChangeVolume(float volume)
    {
        this.volume = volume;
        musicSource.volume = volume;

        PlayerPrefs.SetFloat(PlayerPrefsKeys.playerPrefsVolume, volume);
        PlayerPrefs.Save();
    }

    void SetVolume(float volume)
    {
        this.volume = volume;
        musicSource.volume = volume;
    }

    public void ToggleMusic()
    {
        if (musicPlaying)
            StopMusic();
        else
            PlayMusic();
    }

    void SetMusic(bool musicPlaying)
    {
        if (musicPlaying)
            PlayMusic();
        else
            StopMusic();
    }

    private void PlayMusic()
    {
        musicPlaying = true;
        musicSource.Play();

        PlayerPrefs.SetInt(PlayerPrefsKeys.playerPrefsMusicPlaying, 1);
        PlayerPrefs.Save();
    }

    private void StopMusic()
    {
        musicPlaying = false;
        musicSource.Pause();

        PlayerPrefs.SetInt(PlayerPrefsKeys.playerPrefsMusicPlaying, 0);
        PlayerPrefs.Save();
    }
}
