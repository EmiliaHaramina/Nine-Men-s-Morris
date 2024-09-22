using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectSource : MonoBehaviour
{
    // The source of the sound effect
    [SerializeField] private AudioSource soundSource;
    // The sound effects controller to play a sound effect
    [SerializeField] private SoundEffectsController soundEffectsController;

    public void PlaySoundEffect()
    {
        soundEffectsController.PlaySound(soundSource);
    }
}
