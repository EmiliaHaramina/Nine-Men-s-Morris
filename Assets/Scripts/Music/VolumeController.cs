using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private MusicController musicController;

    void Start()
    {
        scrollbar.onValueChanged.AddListener((float value) => ScrollbarCallback(value));
    }

    void ScrollbarCallback(float value)
    {
        musicController.ChangeVolume(value);
    }
}
