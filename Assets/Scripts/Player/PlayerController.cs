using System.Collections.Generic;
using UnityEngine;

// The player controller is tresponsible for the properties of players
public class PlayerController : MonoBehaviour
{
    // The player properties class contains all properties of a player
    private class PlayerProperties
    {
        // The unique id of the player
        public long id;
        // The player's name
        public string playerName;
        // The player's color hex code
        public string colorHexValue;
        // The player prefs key of the player's name
        public string nameKey;
        // The player prefs key of the player's color
        public string colorKey;

        // A player's properties are defined by the player prefs keys
        // of their name and color
        public PlayerProperties(long id, string nameKey, string colorKey)
        {
            this.id = id;
            this.nameKey = nameKey;
            this.colorKey = colorKey;
        }
    }

    // Two players that can play the game
    private List<PlayerProperties> players = new List<PlayerProperties>();

    // Initializes the player's names and colors according to the player prefs
    public void Initialize()
    {
        PlayerProperties player1 = new PlayerProperties(DefaultValues.player1Id, PlayerPrefsKeys.player1Name, PlayerPrefsKeys.player1Color);
        PlayerProperties player2 = new PlayerProperties(DefaultValues.player2Id, PlayerPrefsKeys.player2Name, PlayerPrefsKeys.player2Color);
        players.Add(player1);
        players.Add(player2);

        SetPlayerNameFromPlayerPrefs(player1);
        SetPlayerColorFromPlayerPrefs(player1);
        SetPlayerNameFromPlayerPrefs(player2);
        SetPlayerColorFromPlayerPrefs(player2);

        // If the players have the same name and/or color (for example, by changing them in
        // the local player prefs file), both of their names and/or colors are changed to their
        // default values
        for (int i = 0; i < players.Count; i++)
        {
            for (int j = 0; j < players.Count; j++)
            {
                if (i == j)
                    continue;

                PlayerProperties iPlayer = players[i];
                PlayerProperties jPlayer = players[j];

                if (iPlayer.playerName.Equals(jPlayer.playerName))
                {
                    SetPlayerName(iPlayer, DefaultValues.player1Name);
                    SetPlayerName(jPlayer, DefaultValues.player2Name);
                }

                if (iPlayer.colorHexValue.Equals(jPlayer.colorHexValue))
                {
                    SetPlayerColor(player1, DefaultValues.player1ColorHexValue);
                    SetPlayerColor(player2, DefaultValues.player2ColorHexValue);
                }
            }
        }
    }

    // Return the name of the player with the given id
    public string GetPlayerName(long id)
    {
        for (int i = 0; i < players.Count; i++)
            if (players[i].id == id)
                return players[i].playerName;
        return null;
    }

    // Returns the color hex code of the player with the given id
    public string GetPlayerColorHexValue(long id)
    {
        for (int i = 0; i < players.Count; i++)
            if (players[i].id == id)
                return players[i].colorHexValue;
        // If the player with this id doesn't exit, return black
        return "#FFFFFF";
    }

    // Sets the player's name from the player prefs
    void SetPlayerNameFromPlayerPrefs(PlayerProperties player)
    {
        player.playerName = PlayerPrefs.GetString(player.nameKey);
    }

    // Sets the player's color from the player prefs
    void SetPlayerColorFromPlayerPrefs(PlayerProperties player)
    {
        player.colorHexValue = PlayerPrefs.GetString(player.colorKey);
    }

    // Sets the player's name as defined by the given string and saves it to player prefs
    void SetPlayerName(PlayerProperties player, string name)
    {
        player.playerName = name;

        PlayerPrefs.SetString(player.nameKey, name);
        PlayerPrefs.Save();
    }

    // Sets the player's color as defined by the given color and saves it to player prefs
    void SetPlayerColor(PlayerProperties player, string colorHexValue)
    {
        player.colorHexValue = colorHexValue;

        PlayerPrefs.SetString(player.colorKey, colorHexValue);
        PlayerPrefs.Save();
    }

    // Returns true if the given name is not currently used by any
    // player
    bool IsNameAvailable(string name)
    {
        for (int i = 0; i < players.Count; i++)
            if (players[i].playerName.Equals(name))
                return false;

        return true;
    }

    // Changes the player's name if it is available and not empty
    // and returns true if it was changed
    public bool ChangePlayerName(long id, string name)
    {
        if (IsNameAvailable(name) && !name.Equals(""))
        {
            for (int i = 0; i < players.Count; i++)
            {
                PlayerProperties player = players[i];
                if (player.id == id)
                {
                    SetPlayerName(player, name);
                    return true;
                }
            }
        }

        return false;
    }

    // Returns true if the given color is not currently used by any
    // player
    bool IsColorAvailable(string colorHexValue)
    {
        for (int i = 0; i < players.Count; i++)
            if (players[i].colorHexValue.Equals(colorHexValue))
                return false;

        return true;
    }

    // Changes the player's color if it is available and returns true
    // if it was changed
    public bool ChangePlayerColor(long id, string colorHexValue)
    {
        if (IsColorAvailable(colorHexValue) && colorHexValue != "")
        {
            for (int i = 0; i < players.Count; i++)
            {
                PlayerProperties player = players[i];
                if (player.id == id)
                {
                    SetPlayerColor(player, colorHexValue);
                    return true;
                }
            }
        }

        return false;
    }
}
