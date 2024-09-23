using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    private bool mainMenuVisible = true;

    void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void ToggleMenu()
    {
        mainMenuVisible = !mainMenuVisible;
        mainMenu.SetActive(mainMenuVisible);
        settingsMenu.SetActive(!mainMenuVisible);
    }
}
