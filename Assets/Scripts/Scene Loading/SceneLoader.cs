using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// The scene loader is responsible for loading the next scene and unloading
// the previous and contains parameters for faded scene loading
public class SceneLoader : MonoBehaviour
{
    // Async operations for loading and unloading scenes
    AsyncOperation loadingOperation;
    AsyncOperation unloadingOperation;
    AsyncOperation unloadingTransitionOperation;

    // Shows the screen for at least minLoadTime seconds
    [SerializeField] private float minLoadTime = 3f;

    // Change Loading... graphic every textChangeLoad seconds
    [SerializeField] private float textChangeLoad = .3f;

    // Timers for loading and fading
    private float loadTimer = 0f;
    private float loadTextTimer = 0f;
    private float fadeTimer = 0f;

    // Loading text game objects
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private TextMeshProUGUI loadPercentText;

    // Fade parameters
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeInTime = .5f;
    [SerializeField] private float fadeOutTime = .5f;

    // TODO: Fade bools to start and stop fading
    private bool fadeIn = true;
    private bool fadeOut = false;

    // Start and finish unloading the previous scene
    private bool unloadStart = true;
    private bool unloadComplete = false;

    // Progress of scene loading
    private float progressValue = 0f;

    // Loading screen texts
    [SerializeField] private List<string> loadingScreenTexts;

    // The update function is used to increment timers and check when they are done
    private void Update()
    {
        // If the unloading of the previous scene is not yet completed and loading of
        // the next scene is not yet started
        if (fadeIn)
        {
            // Fade-in operation adjusts alpha for the canvas group to make the loading
            // scene more visible if the fade in isn't yet complete
            if (loadTimer < fadeInTime)
                canvasGroup.alpha = Mathf.Lerp(0, 1, loadTimer / fadeInTime);
            // Once fade-in is completed
            else
            {
                // Set alpha to 1 so the loading scene is fully visible
                canvasGroup.alpha = 1;
                // Unload the previous scene
                if (unloadStart)
                {
                    unloadingOperation = SceneManager.UnloadSceneAsync(SceneLoadingData.sceneToUnloadName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
                    unloadStart = false;
                }

                // Load the next scene once the previous scene is unloaded
                if (unloadingOperation.isDone)
                {
                    loadingOperation = SceneManager.LoadSceneAsync(SceneLoadingData.sceneToLoadName, LoadSceneMode.Additive);
                    // Prevents the loading screen from flashing too quickly
                    loadingOperation.allowSceneActivation = false;
                    fadeIn = false;
                }
            }
        }
        // If the loading of the next scene has been started
        else
        {
            // Load percent text change
            // The scene loading progress is divided by 0.9, since at 90%, the scene is effectively loaded
            progressValue = Mathf.Clamp01(loadingOperation.progress / 0.9f);
            loadPercentText.text = Mathf.Round(progressValue * 100) + "%";

            // Prevents loading screen from flashing too quickly, even if loading is done
            if ((loadTimer > minLoadTime) && (Mathf.Approximately(loadingOperation.progress, .9f)))
                loadingOperation.allowSceneActivation = true;

            // If scene is loaded, start the fade out process
            if (!fadeOut && loadingOperation.isDone)
            {
                fadeOut = true;
                // Set active scene as the newly loaded scene
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneLoadingData.sceneToLoadName));
            }

            // Fade-out operation adjusts alpha for the canvas group
            if (fadeOut && (fadeTimer < fadeOutTime))
                canvasGroup.alpha = Mathf.Lerp(1, 0, fadeTimer / fadeOutTime);
            // If the fade time has passed, unload the loading scene
            else if (fadeOut && !unloadComplete && (fadeTimer >= fadeOutTime))
            {
                // Once fade-out is complete, set alpha to 0
                canvasGroup.alpha = 0;
                // Unload loading scene
                unloadingTransitionOperation = SceneManager.UnloadSceneAsync(SceneLoadingData.loadingSceneName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
                unloadComplete = true;
            }
        }

        // Set loading scene text based on load time
        loadingText.text = loadingScreenTexts[(int) (loadTextTimer / textChangeLoad) % loadingScreenTexts.Count];

        // Increment load timer for changing loading scene text
        loadTextTimer += Time.deltaTime;

        // Increment total load timer
        loadTimer += Time.deltaTime;

        // Increment fade timer if fade out has started
        if (fadeOut)
            fadeTimer += Time.deltaTime;
    }
}
