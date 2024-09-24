using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// The menu controller is responsible for the menu UI
public class MenuController : MonoBehaviour
{
    // Parameters required for switching between the main menu and
    // the settings menu
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    private bool mainMenuVisible = true;

    // Parameters required for changing the music and sound effects
    [SerializeField] private Sprite speakerOnSprite;
    [SerializeField] private Sprite speakerOffSprite;
    [SerializeField] private Image musicSpeakerImage;
    [SerializeField] private Image soundEffectsSpeakerImage;
    [SerializeField] private bool musicPlaying;
    [SerializeField] private bool soundEffectsPlaying;

    // Parameters for changing player properties
    [SerializeField] private TMP_InputField player1InputField;
    [SerializeField] private Image player1NameAlert;
    [SerializeField] private TMP_InputField player2InputField;
    [SerializeField] private Image player2NameAlert;
    private PlayerController playerController;
    [SerializeField] private Sprite tickSprite;
    [SerializeField] private Sprite crossSprite;

    // When the menu is shown at first, the main menu is set to be active,
    // while the settings menu should not be shown
    public void Initialize()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);

        // Menu player properties initialization
        playerController = FindObjectOfType<PlayerController>();
        player1InputField.text = PlayerPrefs.GetString(PlayerPrefsKeys.player1Name);
        player2InputField.text = PlayerPrefs.GetString(PlayerPrefsKeys.player2Name);
        player1InputField.onValueChanged.AddListener((string value) => ChangePlayer1Name(value));
        player2InputField.onValueChanged.AddListener((string value) => ChangePlayer2Name(value));

        // When the game starts, both players have different names
        player1NameAlert.sprite = tickSprite;
        player2NameAlert.sprite = tickSprite;
    }

    // Toggles between the main menu and the settings menu
    public void ToggleMenu()
    {
        mainMenuVisible = !mainMenuVisible;
        mainMenu.SetActive(mainMenuVisible);
        settingsMenu.SetActive(!mainMenuVisible);

        // When the settings menu is shown again, the last available name of
        // both players will be shown
        player1InputField.text = PlayerPrefs.GetString(PlayerPrefsKeys.player1Name);
        player2InputField.text = PlayerPrefs.GetString(PlayerPrefsKeys.player2Name);
        player1NameAlert.sprite = tickSprite;
        player2NameAlert.sprite = tickSprite;
    }

    // Sets whether the background music is currently playing
    // according to the given bool
    public void SetMusicPlaying(bool musicPlaying)
    {
        this.musicPlaying = musicPlaying;
        ChangeMusicSpeakerImage();
    }

    // Toggles the speaker icon for the background music
    public void ToggleMusicSpeaker()
    {
        musicPlaying = !musicPlaying;
        ChangeMusicSpeakerImage();
    }

    // Changes the background music speaker image according to whether
    // the background music is currently playing
    private void ChangeMusicSpeakerImage()
    {
        if (musicPlaying)
            musicSpeakerImage.sprite = speakerOnSprite;
        else
            musicSpeakerImage.sprite = speakerOffSprite;
    }

    // Sets whether the sound effects are currently playing
    // according to the given bool
    public void SetSoundEffectsPlaying(bool soundEffectsPlaying)
    {
        this.soundEffectsPlaying = soundEffectsPlaying;
        ChangeSoundEffectsSpeakerImage();
    }

    // Toggles the speaker icon for the sound effects
    public void ToggleSoundEffectsSpeaker()
    {
        soundEffectsPlaying = !soundEffectsPlaying;
        if (soundEffectsPlaying)
            soundEffectsSpeakerImage.sprite = speakerOnSprite;
        else
            soundEffectsSpeakerImage.sprite = speakerOffSprite;
    }

    // Changes the sound effects speaker image according to whether
    // the background sound effects is currently playing
    private void ChangeSoundEffectsSpeakerImage()
    {
        if (soundEffectsPlaying)
            soundEffectsSpeakerImage.sprite = speakerOnSprite;
        else
            soundEffectsSpeakerImage.sprite = speakerOffSprite;
    }

    // Changes player 1's name
    private void ChangePlayer1Name(string name)
    {
        if (name.Equals(playerController.GetPlayerName(DefaultValues.player1Id)) ||
            playerController.ChangePlayerName(DefaultValues.player1Id, name))
            player1NameAlert.sprite = tickSprite;
        else
            player1NameAlert.sprite = crossSprite;
    }

    // Changes player 2's name
    private void ChangePlayer2Name(string name)
    {
        if (name.Equals(playerController.GetPlayerName(DefaultValues.player2Id)) ||
            playerController.ChangePlayerName(DefaultValues.player2Id, name))
            player1NameAlert.sprite = tickSprite;
        else
            player2NameAlert.sprite = crossSprite;
    }
}
