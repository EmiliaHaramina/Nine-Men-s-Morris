using UnityEngine;

// The player controller is tresponsible for the properties of players
public class PlayerController : MonoBehaviour
{
    // The player properties class contains all properties of a player
    public class PlayerProperties
    {
        // The player's name
        public string playerName;
        // The player's color
        public Color color;
        // The player prefs key of the player's name
        public string nameKey;
        // The player prefs key of the player's color
        public string colorKey;

        // A player's properties are defined by the player prefs keys
        // of their name and color
        public PlayerProperties(string nameKey, string colorKey)
        {
            this.nameKey = nameKey;
            this.colorKey = colorKey;
        }
    }

    // Two players that can play the game
    private PlayerProperties player1;
    private PlayerProperties player2;

    // Initializes the player's names and colors according to the player prefs
    public void Initialize()
    {
        player1 = new PlayerProperties(PlayerPrefsKeys.player1Name, PlayerPrefsKeys.player1Color);
        player2 = new PlayerProperties(PlayerPrefsKeys.player2Name, PlayerPrefsKeys.player2Color);

        SetPlayerNameFromPlayerPrefs(player1);
        SetPlayerColorFromPlayerPrefs(player1);
        SetPlayerNameFromPlayerPrefs(player2);
        SetPlayerColorFromPlayerPrefs(player2);

        // If the players have the same name and/or color (for example, by changing them in
        // the local player prefs file), both of their names and/or colors are changed to their
        // default values
        if (player1.playerName.Equals(player2.playerName))
        {
            SetPlayerName(player1, DefaultValues.player1Name);
            SetPlayerName(player2, DefaultValues.player2Name);
        }
        if (player1.color.Equals(player2.color))
        {
            ColorUtility.TryParseHtmlString(DefaultValues.player1Color, out Color player1Color);
            SetPlayerColor(player1, player1Color);

            ColorUtility.TryParseHtmlString(DefaultValues.player1Color, out Color player2Color);
            SetPlayerColor(player2, player2Color);
        }
    }

    // Getters for players 1 and 2
    public PlayerProperties GetPlayer1 () { return player1; }
    public PlayerProperties GetPlayer2 () { return player2; }

    // Sets the player's name from the player prefs
    void SetPlayerNameFromPlayerPrefs(PlayerProperties player)
    {
        player.playerName = PlayerPrefs.GetString(player.nameKey);
    }

    // Sets the player's color from the player prefs
    void SetPlayerColorFromPlayerPrefs(PlayerProperties player)
    {
        ColorUtility.TryParseHtmlString(PlayerPrefs.GetString(player.colorKey), out player.color);
    }

    // Sets the player's name as defined by the given string and saves it to player prefs
    void SetPlayerName(PlayerProperties player, string name)
    {
        player.playerName = name;

        PlayerPrefs.SetString(player.nameKey, name);
        PlayerPrefs.Save();
    }

    // Sets the player's color as defined by the given color and saves it to player prefs
    void SetPlayerColor(PlayerProperties player, Color color)
    {
        player.color = color;

        Debug.Log(ColorUtility.ToHtmlStringRGB(color));
        PlayerPrefs.SetString(player.colorKey, ColorUtility.ToHtmlStringRGB(color));
        PlayerPrefs.Save();
    }

    // Returns true if the given name is not currently used by any
    // player
    bool IsNameAvailable(string name)
    {
        if (player1.playerName.Equals(name))
            return false;
        if (player2.playerName.Equals(name))
            return false;

        return true;
    }

    // Changes the player's name if it is available and returns true
    // if it was changed
    public bool ChangePlayerName(PlayerProperties player, string name)
    {
        if (IsNameAvailable(name))
        {
            SetPlayerName(player, name);
            return true;
        }
        return false;
    }

    // Returns true if the given color is not currently used by any
    // player
    bool IsColorAvailable(Color color)
    {
        if (player1.color.Equals(color))
            return false;
        if (player2.color.Equals(color))
            return false;

        return true;
    }

    // Changes the player's color if it is available and returns true
    // if it was changed
    public bool ChangePlayerColor(PlayerProperties player, Color color)
    {
        if (IsColorAvailable(color))
        {
            SetPlayerColor(player, color);
            return true;
        }
        return false;
    }
}
