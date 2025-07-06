using UnityEngine;

public class Sink : ChoreBase
{
    [Header("Visuals")]
    [SerializeField] private GameObject sinkWaterToggle;  // Visual water stream
    [SerializeField] private GameObject sinkBubbles;      // GameObject holding particle system

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string onTrigger = "TurnOnSink";
    [SerializeField] private string offTrigger = "TurnOffSink";

    public static bool IsSinkOn { get; private set; } = false;

    public override void StartChore()
    {
        base.StartChore();

        if (!IsSinkOn)
            TriggerOnAnimation();
        else
            TriggerOffAnimation();
    }

    public override void CompleteChore()
    {
        base.CompleteChore();

        IsSinkOn = !IsSinkOn;

        // Toggle visuals
        if (sinkWaterToggle != null)
            sinkWaterToggle.SetActive(IsSinkOn);

        if (sinkBubbles != null)
            sinkBubbles.SetActive(IsSinkOn);

        Debug.Log("Sink is now " + (IsSinkOn ? "ON" : "OFF"));

        // Trigger animation
        if (IsSinkOn)
            TriggerOnAnimation();
        else
            TriggerOffAnimation();

        // Optional: Reset triggers if needed
        if (!IsSinkOn && animator != null)
        {
            animator.ResetTrigger(onTrigger);
            animator.ResetTrigger(offTrigger);
        }
    }

    private void TriggerOnAnimation()
    {
        if (animator != null && !string.IsNullOrEmpty(onTrigger))
        {
            Debug.Log("Triggering TurnOnSink animation");
            animator.SetTrigger(onTrigger);
        }
    }

    private void TriggerOffAnimation()
    {
        if (animator != null && !string.IsNullOrEmpty(offTrigger))
        {
            Debug.Log("Triggering TurnOffSink animation");
            animator.SetTrigger(offTrigger);
        }
    }
}
