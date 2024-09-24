using UnityEngine;
using UnityEngine.UI;

// The color choice contains a color and its hex value in
// string form
public class ColorChoice : MonoBehaviour
{
    // The image that a color is shown in
    [SerializeField] Image colorImage;
    // The color this color choice represents
    private Color color;
    // The string hex value of the color
    private string colorHexValue;

    // Sets the color of this color choice by taking
    // the color of the colorImage
    void Start()
    {
        color = colorImage.color;
        colorHexValue = "#" + ColorUtility.ToHtmlStringRGB(color);
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
}
