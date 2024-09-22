using UnityEngine;
using UnityEngine.UI;

// The sound effects volume controller is responsible for controling
// the volume of sound effects
public class SoundEffectsVolumeController : MonoBehaviour
{
    // The scrollbar that is used for changing the volume
    [SerializeField] private Scrollbar scrollbar;
    // The sound effects controller responsible for the sound effects
    [SerializeField] private SoundEffectsController soundEffectsController;

    // Initializes the sound effects volume scrollbar
    public void Initialize()
    {
        scrollbar.value = PlayerPrefs.GetFloat(PlayerPrefsKeys.soundEffectsVolume);
        scrollbar.onValueChanged.AddListener((float value) => ScrollbarCallback(value));
    }

    // Changes the volume of sound effects every time the scrollbar value is changed
    void ScrollbarCallback(float value)
    {
        // Changes the volume of sound effects
        soundEffectsController.SetVolume(value);
    }
}
