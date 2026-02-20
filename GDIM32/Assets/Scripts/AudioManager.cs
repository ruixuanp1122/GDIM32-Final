using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sound Effects")]
    public AudioClip orderReceivedClip;
    public AudioClip foodPickupClip;
    public AudioClip tipReceivedClip;
    public AudioClip basePayReceivedClip;
    public AudioClip cookiePickupClip;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void PlayOrderReceived() => PlaySound(orderReceivedClip);
    public void PlayFoodPickup() => PlaySound(foodPickupClip);
    public void PlayTipReceived() => PlaySound(tipReceivedClip);
    public void PlayBasePayReceived() => PlaySound(basePayReceivedClip);
    public void PlayCookiePickup() => PlaySound(cookiePickupClip);
}