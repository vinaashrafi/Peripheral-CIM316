using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Sound Clips")]
    public AudioClip doorOpenClip;
    public AudioClip doorCloseClip;

    [Header("Footstep Sounds")]
    public AudioClip[] footstepClips;
    
    [Header("Curtain Sounds")]
    public AudioClip curtainOpenClip;
    public AudioClip curtainCloseClip;


    
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

        // Instantiate the AudioSource prefab at position
        AudioSource source = Instantiate(audioSourcePrefab, position, Quaternion.identity);
        source.clip = clip;
        source.Play();

        // Destroy the AudioSource game object after clip length
        Destroy(source.gameObject, clip.length);
    }

    public void PlayDoorSound(bool opening, Vector3 position)
    {
        AudioClip clipToPlay = opening ? doorOpenClip : doorCloseClip;
        PlaySoundAtPosition(clipToPlay, position);
    }

    public void PlayFootstepSound(Vector3 position)
    {
        if (footstepClips.Length == 0 || audioSourcePrefab == null) return;

        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        PlaySoundAtPosition(clip, position);
    }
    public void PlayCurtainSound(bool closing, Vector3 position)
    {
        AudioClip clipToPlay = closing ? curtainCloseClip : curtainOpenClip;
        PlaySoundAtPosition(clipToPlay, position);
    }
    
    
    
}
