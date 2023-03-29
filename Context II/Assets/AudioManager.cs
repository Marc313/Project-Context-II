using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip oofSound;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayOofSound()
    {
        source.PlayOneShot(oofSound);
    }
}
