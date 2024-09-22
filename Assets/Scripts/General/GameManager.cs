using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MusicController musicController;
    [SerializeField] private VolumeController volumeController;

    void Start()
    {
        PlayerPrefsKeys.Initialize();
        musicController.Initialize();
        volumeController.Initialize();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
