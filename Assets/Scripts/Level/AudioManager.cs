using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _levelAudioClip;
    [SerializeField] private AudioClip _pauseAudioClip;
    [SerializeField] private AudioClip _endAudioClip;

    [Header("Variables")]
    public bool gamePaused;

    private void Awake()
    {
        AudioManagerInitialization();
    }

    private void Update()
    {
        if (gamePaused && _audioSource.clip != _levelAudioClip)
        {
            _audioSource.clip = _levelAudioClip;
            _audioSource.Play();
        }
        else if (!gamePaused && _audioSource.clip != _pauseAudioClip)
        {
            _audioSource.clip = _pauseAudioClip;
            _audioSource.Play();
        }
    }

    private void AudioManagerInitialization()
    {
        gamePaused = false;
    }
}
