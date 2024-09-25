using System.Collections.Generic;
using TMPro;
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

    [SerializeField] private List<ColorChoice> player1ColorChoices;
    [SerializeField] private List<ColorChoice> player2ColorChoices;

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

        // Setting the player ids of color choices
        long player1Id = DefaultValues.player1Id;
        long player2Id = DefaultValues.player2Id;

        foreach (ColorChoice colorChoice in player1ColorChoices)
            colorChoice.SetPlayerId(player1Id);
        foreach (ColorChoice colorChoice in player2ColorChoices)
            colorChoice.SetPlayerId(player2Id);

        // Setting the color choices as picked and unavailable
        string player1ColorHexValue = playerController.GetPlayerColorHexValue(player1Id);
        string player2ColorHexValue = playerController.GetPlayerColorHexValue(player2Id);
        for (int i = 0; i < player1ColorChoices.Count; i++)
        {
            ColorChoice colorChoice = player1ColorChoices[i];
            if (colorChoice.GetColorHexValue() == player1ColorHexValue)
                colorChoice.MakePicked();
            else if (colorChoice.GetColorHexValue() == player2ColorHexValue)
                colorChoice.MakeUnavailable();
        }
        for (int i = 0; i < player2ColorChoices.Count; i++)
        {
            ColorChoice colorChoice = player2ColorChoices[i];
            if (colorChoice.GetColorHexValue() == player2ColorHexValue)
                colorChoice.MakePicked();
            else if (colorChoice.GetColorHexValue() == player1ColorHexValue)
                colorChoice.MakeUnavailable();
        }
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

    // Changes a player's color to the defined color and changes the color
    // of color choice panels according to the picked color
    public void ChangePlayerColor(long playerId, string colorHexValue)
    {
        if (playerController.ChangePlayerColor(playerId, colorHexValue))
        {
            // Switch all color choices
            SwitchColorChoices(playerId, colorHexValue, player1ColorChoices);
            SwitchColorChoices(playerId, colorHexValue, player2ColorChoices);
        }
    }

    // Switches all color choices from the given list according to the color that was just changed
    // The color that was changed is defined by its colorHexValue, while the playerId is the id
    // of the player that changed their color
    private void SwitchColorChoices(long playerId, string colorHexValue, List<ColorChoice> colorchoices)
    {
        ColorChoice colorChoicePicked = FindColorChoice(playerId, colorHexValue);
        foreach (ColorChoice colorChoice in colorchoices)
        {
            // If this is the color choice that was just picked, set it to picked
            if (colorChoice == colorChoicePicked && colorChoice.GetPlayerId() == playerId)
                colorChoice.MakePicked();
            // If this is the color that the player had picked before this one, set it
            // to available
            else if (colorChoice.IsPicked() && colorChoice.GetPlayerId() == playerId)
                colorChoice.MakeAvailable();
            // If this color choice has the same color as the one that was just picked, but
            // is for the other player, set it to unavailable
            else if (colorChoice.GetColorHexValue() == colorHexValue)
                colorChoice.MakeUnavailable();
            // If this is the color that was unavailable for the other player before,
            // set it to available
            else if (colorChoice.IsUnavailable() && colorChoice.GetPlayerId() != playerId)
                colorChoice.MakeAvailable();
        }
    }

    // Returns the color choice corresponding to the player with the given id that
    // contains the given color hex value
    private ColorChoice FindColorChoice(long playerId, string colorHexValue)
    {
        if (playerId == DefaultValues.player1Id)
        {
            foreach (ColorChoice colorChoice1 in player1ColorChoices)
                if (colorChoice1.GetPlayerId() == playerId &&
                    colorChoice1.GetColorHexValue().Equals(colorHexValue))
                    return colorChoice1;
        }
        else if (playerId == DefaultValues.player2Id)
        {
            foreach (ColorChoice colorChoice2 in player2ColorChoices)
                if (colorChoice2.GetPlayerId() == playerId &&
                    colorChoice2.GetColorHexValue().Equals(colorHexValue))
                    return colorChoice2;
        }
        return null;
    }
}
