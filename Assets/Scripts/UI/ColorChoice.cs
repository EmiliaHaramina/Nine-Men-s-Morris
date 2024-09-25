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
    private MenuController menuController;

    // Sets the color of this color choice by taking
    // the color of the colorImage
    void Awake()
    {
        menuController = FindObjectOfType<MenuController>();

        color = colorImage.color;
        colorHexValue = "#" + ColorUtility.ToHtmlStringRGB(color);
    }

    // Sets the id of the plahyer this color choice is for
    public void SetPlayerId(long playerId)
    {
        this.playerId = playerId;
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
    private void ChangePanelColor(Color color)
    {
        // New color is used because panel.color = color results in a white color
        panel.color = new Color(color.r * 1f / 255, color.g * 1f / 255, color.b * 1f / 255);
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
        ChangePanelColor(DefaultValues.colorAvailable);
        EnablePanelButton();
    }

    // Changes the color of the surrounding panel to
    // red and makes it uninteractable
    public void MakeUnavailable()
    {
        ChangePanelColor(DefaultValues.colorUnavailable);
        DisablePanelButton();
    }


    // Changes the color of the surrounding panel to
    // green and makes it uninteractable
    public void MakePicked()
    {
        ChangePanelColor(DefaultValues.colorPicked);
        DisablePanelButton();
    }

    // Changes the player's color
    public void ChangePlayerColor()
    {
        menuController.ChangePlayerColor(playerId, colorHexValue);
    }
}
