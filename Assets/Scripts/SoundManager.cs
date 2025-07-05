using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Sound Clips")]
    public AudioClip doorOpenClip;
    public AudioClip doorCloseClip;

    [Header("Audio Settings")]
    public AudioSource audioSourcePrefab;

    private void Awake()
    {
        // Basic Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlaySoundAtPosition(AudioClip clip, Vector3 position)
    {
        if (clip == null || audioSourcePrefab == null) return;

        AudioSource source = Instantiate(audioSourcePrefab, position, Quaternion.identity);
        source.clip = clip;
        source.Play();
        Destroy(source.gameObject, clip.length);
    }

    public void PlayDoorSound(bool opening, Vector3 position)
    {
        AudioClip clipToPlay = opening ? doorOpenClip : doorCloseClip;
        PlaySoundAtPosition(clipToPlay, position);
    }
}
