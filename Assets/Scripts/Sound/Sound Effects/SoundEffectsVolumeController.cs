using UnityEngine;
using UnityEngine.UI;

// The sound effects volume controller is responsible for controling
// the volume of sound effects
public class SoundEffectsVolumeController : MonoBehaviour
{
    // The scrollbar that is used for changing the volume
    [SerializeField] private Scrollbar scrollbar;
    // The sound effects controller responsible for the sound effects
    private SoundEffectsController soundEffectsController;
    // Saves the previous volume value before disabling the volume
    private float lastVolume;

    // Finds the sound effects controller and initializes the sound effects volume scrollbar
    public void Initialize()
    {
        soundEffectsController = FindAnyObjectByType<SoundEffectsController>();

        scrollbar.value = PlayerPrefs.GetFloat(PlayerPrefsKeys.soundEffectsVolume);
        lastVolume = scrollbar.value;
        scrollbar.onValueChanged.AddListener((float value) => ScrollbarCallback(value));
    }

    // Changes the volume of sound effects every time the scrollbar value is changed
    // if the scrollbar is interactable (not disabled)
    void ScrollbarCallback(float value)
    {
        if (scrollbar.interactable)
            // Changes the volume of sound effects
            soundEffectsController.SetVolume(value);
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
