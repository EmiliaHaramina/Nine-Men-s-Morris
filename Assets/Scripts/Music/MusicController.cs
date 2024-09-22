using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private float volume;
    [SerializeField] private bool musicPlaying;

    void Start()
    {
        if (musicPlaying)
            musicSource.Play();
    }

    public float GetVolume()
    {
        return volume;
    }

    public void ChangeVolume(float volume)
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

    private void PlayMusic()
    {
        musicPlaying = true;
        musicSource.UnPause();
    }

    private void StopMusic()
    {
        musicPlaying = false;
        musicSource.Pause();
    }
}
