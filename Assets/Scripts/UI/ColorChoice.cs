using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// The color choice contains a color and its surrounding
// frame and button
public class ColorChoice : MonoBehaviour
{
    // The image that a color is shown in
    [SerializeField] Image colorImage;
    // The color this color choice represents
    private Color color;
    // The string hex value of the color
    private string colorHexValue;
    // The panel surrounding the color
    [SerializeField] private Image panel;
    // Id of the player this color choice is for
    private long playerId;
    // The menu controller that is called when a color choice is clicked
    private MenuManager menuManager;

    // An enum for defining the state of the color choice
    private enum State
    {
        Available, Picked, Unavailable
    }

    // The state of the color choice
    private State state;

    // Sets the color of this color choice by taking
    // the color of the colorImage and sets the state to
    // available
    void Awake()
    {
        menuManager = FindObjectOfType<MenuManager>();

        color = colorImage.color;
        colorHexValue = "#" + ColorUtility.ToHtmlStringRGB(color);

        state = State.Available;
    }

    // Sets the id of the player this color choice is for
    public void SetPlayerId(long playerId)
    {
        this.playerId = playerId;
    }

    // Returns the id of the player this color choice is for
    public long GetPlayerId()
    {
        return playerId;
    }

    // Returns the color
    public Color GetColor()
    {
        return color;
    }

    // Returns the string hex value of the color
    public string GetColorHexValue()
    {
        return colorHexValue;
    }

    // Changes the color of the surrounding panel
    // to the defined color
    private void ChangePanelColor(string colorHexValue)
    {
        ColorUtility.TryParseHtmlString(colorHexValue, out Color color);
        // Added transparency to the panel because it is not stored in the hex value
        color.a = DefaultValues.panelColorTransparency / 255f;
        panel.color = color;
    }

    // Disables the panel button and its sound effects
    private void DisablePanelButton()
    {
        panel.GetComponent<Button>().enabled = false;
        panel.GetComponent<EventTrigger>().enabled = false;
    }

    // Enables the panel button and its sound effects
    private void EnablePanelButton()
    {
        panel.GetComponent<Button>().enabled = true;
        panel.GetComponent<EventTrigger>().enabled = true;
    }

    // Changes the color of the surrounding panel to
    // gray and makes it interactable
    public void MakeAvailable()
    {
        ChangePanelColor(DefaultValues.colorAvailableHexValue);
        EnablePanelButton();
        state = State.Available;
    }

    // Changes the color of the surrounding panel to
    // red and makes it uninteractable
    public void MakeUnavailable()
    {
        ChangePanelColor(DefaultValues.colorUnavailableHexValue);
        DisablePanelButton();
        state = State.Unavailable;
    }


    // Changes the color of the surrounding panel to
    // green and makes it uninteractable
    public void MakePicked()
    {
        ChangePanelColor(DefaultValues.colorPickedHexValue);
        DisablePanelButton();
        state = State.Picked;
    }

    // Changes the player's color
    public void ChangePlayerColor()
    {
        menuManager.ChangePlayerColor(playerId, colorHexValue);
    }

    // Returns true if the state of the color choice is picked
    public bool IsPicked()
    {
        return state == State.Picked;
    }

    // Returns true if the state of the color choice is available
    public bool IsAvailable()
    {
        return state == State.Available;
    }

    // Returns true if the state of the color choice is unavailable
    public bool IsUnavailable()
    {
        return state == State.Unavailable;
    }
}
