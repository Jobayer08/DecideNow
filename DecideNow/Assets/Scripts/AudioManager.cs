using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip buttonClick;
    public AudioClip correct;
    public AudioClip wrong;
    public AudioClip timerWarning;
    public AudioClip backgroundMusic;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayButtonClick()
    {
        sfxSource.PlayOneShot(buttonClick);
    }

    public void PlayCorrect()
    {
        sfxSource.PlayOneShot(correct);
    }

    public void PlayWrong()
    {
        sfxSource.PlayOneShot(wrong);
    }

    public void PlayTimerWarning()
    {
        sfxSource.PlayOneShot(timerWarning);
    }
}