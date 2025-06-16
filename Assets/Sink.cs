using UnityEngine;

public class Sink : ChoreBase
{
    [SerializeField] private GameObject sinkWaterToggle;      // Visual water stream
    [SerializeField] private GameObject sinkBubbles;      // GameObject holding particle system

    public static bool IsSinkOn { get; private set; } = false;

    public override void CompleteChore()
    {
        base.CompleteChore();

        IsSinkOn = !IsSinkOn; // Flip sink state

        // Toggle water visual
        if (sinkWaterToggle != null)
            sinkWaterToggle.SetActive(IsSinkOn);

        // Toggle particle GameObject
        if (sinkBubbles != null)
            sinkBubbles.SetActive(IsSinkOn);

        Debug.Log("Sink is now " + (IsSinkOn ? "ON" : "OFF"));
    }
    

    // public override void StartChore()
    // {
    //     base.StartChore();
    //
    //     if (sinkObjectToToggle != null)
    //         sinkObjectToToggle.SetActive(true); // Turn it ON when the chore starts
    // }
    //
    // public override void CompleteChore()
    // {
    //     base.CompleteChore();
    //
    //     if (sinkObjectToToggle != null)
    //         sinkObjectToToggle.SetActive(false); // Turn it OFF when the chore finishes
    // }
    //
    // public override void StopChore()
    // {
    //     base.StopChore();
    //
    //     if (sinkObjectToToggle != null)
    //         sinkObjectToToggle.SetActive(false); // Optional: also turn it off if the chore gets canceled
    // }
}