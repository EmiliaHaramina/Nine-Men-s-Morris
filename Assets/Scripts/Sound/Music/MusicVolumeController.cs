using UnityEngine;
using UnityEngine.UI;

// The music volume controller is responsible for controling the volume of
// the background music
public class MusicVolumeController : MonoBehaviour
{
    // The scrollbar that is used for changing the volume
    [SerializeField] private Scrollbar scrollbar;
    // The music controller responsible for the background music
    [SerializeField] private MusicController musicController;

    // Initializes the background music volume scrollbar
    public void Initialize()
    {
        scrollbar.value = PlayerPrefs.GetFloat(PlayerPrefsKeys.playerPrefsMusicVolume);
        scrollbar.onValueChanged.AddListener((float value) => ScrollbarCallback(value));
    }

    // Changes the volume of the background music every time the scrollbar value is changed
    void ScrollbarCallback(float value)
    {
        // Changes the volume of the background music
        musicController.SetVolume(value);
    }
}
