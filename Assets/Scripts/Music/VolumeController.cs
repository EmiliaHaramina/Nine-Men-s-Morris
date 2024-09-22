using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private MusicController musicController;

    public void Initialize()
    {
        scrollbar.value = PlayerPrefs.GetFloat(PlayerPrefsKeys.playerPrefsVolume);
        scrollbar.onValueChanged.AddListener((float value) => ScrollbarCallback(value));
    }

    void ScrollbarCallback(float value)
    {
        musicController.ChangeVolume(value);
    }
}
