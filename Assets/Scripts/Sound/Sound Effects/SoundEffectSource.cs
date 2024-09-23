using UnityEngine;

// The sound effect source allows an object to play a sound effect
public class SoundEffectSource : MonoBehaviour
{
    // The source of the sound effect
    [SerializeField] private AudioSource soundSource;
    // The sound effects controller to play a sound effect
    private SoundEffectsController soundEffectsController;

    // Finds the sound effect controller
    private void Start()
    {
        soundEffectsController = FindObjectOfType<SoundEffectsController>();
    }

    // Plays the object's sound effect
    public void PlaySoundEffect()
    {
        soundEffectsController.PlaySound(soundSource);
    }
}
