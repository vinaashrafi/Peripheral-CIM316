using UnityEngine;

public class cat : ChoreBase
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pettingSound;

    public override void StartChore()
    {
        base.StartChore();

        if (animator != null)
            animator.SetBool("isPetting", true);

        if (audioSource != null && pettingSound != null)
            audioSource.PlayOneShot(pettingSound);

        Debug.Log("Cat chore started: isPetting set to true.");
    }

    public override void StopChore()
    {
        base.StopChore();

        if (animator != null)
            animator.SetBool("isPetting", false);
        Debug.Log("Cat chore stopped: isPetting set to false.");
    }

    public override void CompleteChore()
    {
        base.CompleteChore();

        if (animator != null)
            animator.SetBool("isPetting", false);
        Debug.Log("Cat chore completed: isPetting set to false.");
    }
}