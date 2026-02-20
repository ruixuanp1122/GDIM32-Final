using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("WAV Sound Effects")]
    public AudioClip orderReceived;
    public AudioClip foodPickup;
    public AudioClip tipReceived;
    public AudioClip basePayReceived;
    public AudioClip cookiePickup;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    private void PlayClip(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void PlayOrderReceived() => PlayClip(orderReceived);
    public void PlayFoodPickup() => PlayClip(foodPickup);
    public void PlayTipReceived() => PlayClip(tipReceived);
    public void PlayBasePayReceived() => PlayClip(basePayReceived);
    public void PlayCookiePickup() => PlayClip(cookiePickup);
}