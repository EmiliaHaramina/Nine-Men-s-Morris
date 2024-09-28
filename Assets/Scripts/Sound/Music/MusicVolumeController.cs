using UnityEngine;
using UnityEngine.UI;

// The music volume controller is responsible for controling the volume of
// the background music
public class MusicVolumeController : MonoBehaviour
{
    // The scrollbar that is used for changing the volume
    [SerializeField] private Scrollbar scrollbar;
    // The music controller responsible for the background music
    private MusicController musicController;
    // Saves the previous volume value before disabling the volume
    private float lastVolume;

    // Finds the music controller and initializes the background music volume scrollbar
    public void Initialize()
    {
        musicController = FindAnyObjectByType<MusicController>();

        scrollbar.value = PlayerPrefs.GetFloat(PlayerPrefsKeys.musicVolume);
        lastVolume = scrollbar.value;
        scrollbar.onValueChanged.AddListener((float value) => ScrollbarCallback(value));
    }

    // Changes the volume of the background music every time the scrollbar value is changed
    // if the scrollbar is interactable (not disabled)
    void ScrollbarCallback(float value)
    {
        if (scrollbar.interactable)
            // Changes the volume of the background music
            musicController.SetVolume(value);
    }

    // Disables the volume scrollbar and sets the value to 0
    public void DisableVolume()
    {
        scrollbar.interactable = false;
        lastVolume = scrollbar.value;
        scrollbar.value = 0;
    }

    // Enables the volume scrollbar and sets the volume back to the previous value
    public void EnableVolume()
    {
        scrollbar.value = lastVolume;
        scrollbar.interactable = true;
    }
}
