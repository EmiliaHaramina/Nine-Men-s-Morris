using System.Collections;
using UnityEngine;

// The menu scene loader is used by menus to load a scene after
// a certain time passes
public class MenuSceneLoader : MonoBehaviour
{
    // Next scene load trigger
    [SerializeField] private SceneLoadTrigger sceneLoadTrigger;
    [SerializeField] private float waitTimeBeforeLoad = 0f;

    // Loads the next scene
    public void LoadNextScene()
    {
        // Starts coroutine that loads the next scene
        StartCoroutine(LoadNextSceneAfterTime(waitTimeBeforeLoad));
    }

    // Loads the next scene after the wait time
    IEnumerator LoadNextSceneAfterTime(float waitTimeBeforeLoad)
    {
        // Yield on a new YieldInstruction that waits for waitTimeBeforeLoad seconds
        yield return new WaitForSeconds(waitTimeBeforeLoad);

        // Load next scene
        sceneLoadTrigger.LoadSceneWithLoadingScreen();
    }
}
