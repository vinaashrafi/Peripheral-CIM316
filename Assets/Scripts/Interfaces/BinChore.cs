using System.Collections;
using UnityEngine;

public class BinChore : ChoreBase
{
    public Animator animator;
    public string openTrigger = "OpenBin";
    public string closeTrigger = "CloseBin";

    public bool isBinOpen = false;

    public Collider binBagCollider; // Drag the bin bag collider here in the inspector
    
    
    public override void StartChore()
    {
        base.StartChore();
        
        // bin bag collider starts disabled
        if (binBagCollider != null)
            binBagCollider.enabled = false;
        
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

        // Toggle the bin bag collider
        if (binBagCollider != null)
            binBagCollider.enabled = isBinOpen;
        if (PeripheralGameManager.Instance != null)
        {
            PeripheralGameManager.Instance.RainStart();
        }
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

    // Called via Animation Event
    public void TurnOnBinBagCollider()
    {
        if (binBagCollider != null)
            binBagCollider.enabled = true;
    }

    // Called via Animation Event
    public void TurnOffBinBagCollider()
    {
        if (binBagCollider != null)
            binBagCollider.enabled = false;
    }
}