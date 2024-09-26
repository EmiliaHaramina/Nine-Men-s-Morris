using UnityEngine;

abstract public class MenuManager : MonoBehaviour
{
    public abstract void SetMusicPlaying(bool musicPlaying);
    public abstract void SetSoundEffectsPlaying(bool soundEffectsPlaying);
}
