using UnityEngine;

public class Sink : ChoreBase
{
    [SerializeField] private GameObject sinkObjectToToggle;
    
    private bool isSinkOn = false;

    public override void CompleteChore()
    {
        base.CompleteChore();

        isSinkOn = !isSinkOn; // Flip the state
        if (sinkObjectToToggle != null)
            sinkObjectToToggle.SetActive(isSinkOn);
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