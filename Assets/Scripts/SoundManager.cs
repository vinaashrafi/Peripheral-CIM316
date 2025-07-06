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

    [Header("Printer Sounds")]
    public AudioClip printerClip;
    
    [Header("Computer / CCTV Sounds")]
    public AudioClip computerOnClip;
    public AudioClip computerOffClip;
    public AudioClip cctvViewClip;
    public AudioClip SwitchCameraClip;
    
    [Header("Sink Sounds")]
    public AudioClip sinkOnClip;
    public AudioClip sinkOffClip;
    private AudioSource sinkAudioSource;
    
    [Header("Audio Settings")]
    public AudioSource audioSourcePrefab;
    
    // Dedicated AudioSource for CCTV loop sound
    private AudioSource cctvAudioSource;
    
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
    
    public void PlayPrinterSound(Vector3 position)
    {
        PlaySoundAtPosition(printerClip, position);
    }
    public void PlayComputerOnSound(Vector3 position)
    {
        PlaySoundAtPosition(computerOnClip, position);
    }
    public void PlayComputerOffSound(Vector3 position)
    {
        PlaySoundAtPosition(computerOffClip, position);
    }
    // CCTV sound play (looping)


    public void SwitchCameraSound(Vector3 position)
    {
        PlaySoundAtPosition(SwitchCameraClip, position);
    }

    public void PlayCCTVLoopSound(Vector3 position)
    {
        if (cctvAudioSource == null && audioSourcePrefab != null && cctvViewClip != null)
        {
            cctvAudioSource = Instantiate(audioSourcePrefab, position, Quaternion.identity);
            cctvAudioSource.clip = cctvViewClip;
            cctvAudioSource.loop = true;
            cctvAudioSource.Play();
            Debug.Log("CCTV sound playing");
        }
    }

    public void StopCCTVLoopSound()
    {
        if (cctvAudioSource != null)
        {
            cctvAudioSource.Stop();
            Destroy(cctvAudioSource.gameObject);
            cctvAudioSource = null;
            Debug.Log("CCTV sound stopped");
        }
    }
    


    public void StartSinkLoopSound(Vector3 position)
    {
        if (sinkAudioSource == null && audioSourcePrefab != null && sinkOnClip != null)
        {
            sinkAudioSource = Instantiate(audioSourcePrefab, position, Quaternion.identity);
            sinkAudioSource.clip = sinkOnClip;
            sinkAudioSource.loop = true;
            sinkAudioSource.Play();
            Debug.Log("Sink loop sound playing");
        }
    }

    public void StopSinkLoopSound()
    {
        if (sinkAudioSource != null)
        {
            sinkAudioSource.Stop();
            Destroy(sinkAudioSource.gameObject);
            sinkAudioSource = null;
            Debug.Log("Sink loop sound stopped");
        }
    }
    
}
