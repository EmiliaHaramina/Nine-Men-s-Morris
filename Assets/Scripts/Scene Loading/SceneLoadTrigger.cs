using UnityEngine;
using UnityEngine.SceneManagement;

// The scene load trigger contains function that start scene loading
public class SceneLoadTrigger : MonoBehaviour
{
    // The name of the scene that needs to be loaded next
    [SerializeField] private string targetSceneName;

    // Loads the next scene with a loading screen in between
    public void LoadSceneWithLoadingScreen()
    {
        // Sets the names of the scenes that need to be loaded and unloaded
        SceneLoadingData.sceneToLoadName = targetSceneName;
        SceneLoadingData.sceneToUnloadName = SceneManager.GetActiveScene().name;

        // Loads the loading screen
        SceneManager.LoadSceneAsync(SceneLoadingData.loadingSceneName, LoadSceneMode.Additive);
    }

    // Loads the next scene without a loading screen in between
    public void LoadSceneWithoutLoadingScreen()
    {
        // Sets the name of the scene that needs to be loaded
        SceneLoadingData.sceneToLoadName = targetSceneName;

        // Loads the scene that needs to be loaded
        SceneManager.LoadSceneAsync(SceneLoadingData.sceneToLoadName, LoadSceneMode.Single);
    }
}
