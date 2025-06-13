using System.Collections;
using UnityEngine;

public class BinChore : ChoreBase
{
    public Animator animator;
    public string openTrigger = "OpenBin";
    public string closeTrigger = "CloseBin";

    public bool isBinOpen = false;

    public override void StartChore()
    {
        base.StartChore();

        if (!isBinOpen)
            TriggerOpenAnimation();
        else
            TriggerCloseAnimation();
    }

    public override void CompleteChore()
    {
        base.CompleteChore();

        isBinOpen = !isBinOpen;
        Debug.Log($"CompleteChore called. Bin is now {(isBinOpen ? "OPEN" : "CLOSED")}");

        // Only reset triggers if bin is now closed
        if (!isBinOpen && animator != null)
        {
            animator.ResetTrigger(openTrigger);
            animator.ResetTrigger(closeTrigger);
        }
    }

    public void TriggerOpenAnimation()
    {
        if (animator != null && !string.IsNullOrEmpty(openTrigger))
        {
            Debug.Log("Triggering OpenBin animation");
            animator.SetTrigger(openTrigger);
        }
    }

    public void TriggerCloseAnimation()
    {
        if (animator != null && !string.IsNullOrEmpty(closeTrigger))
        {
            Debug.Log("Triggering CloseBin animation");
            animator.SetTrigger(closeTrigger);
        }
    }
}