using System;
using UnityEngine;

public abstract class ChoreBase : MonoBehaviour, IChoreable, IInteractable
{
    public float timeToComplete = 3f;
    public float currentProgress = 0f;
    public bool isWorking = false;

    public Animator animator;
    public string openTrigger = "OpenBin";
    public string closeTrigger = "CloseBin";

    public bool isBinOpen = false;

    public event Action<float> OnChoreProgress;
    public event Action OnChoreStarted;
    public event Action OnChoreStopped;
    public event Action OnChoreCompleted;

    public bool IsChoreActive()
    {
        return isWorking;
    }

    void Update()
    {
   
        if (isWorking)
        {
            currentProgress += Time.deltaTime;
            OnChoreProgress?.Invoke(currentProgress / timeToComplete);

            if (currentProgress >= timeToComplete)
            {
                CompleteChore();
            }
        }
    }

    public virtual void StartChore()
    {
        if (isWorking) return;

        isWorking = true;
        currentProgress = 0f;
        OnChoreStarted?.Invoke();

        // If bin is closed, open it; if open, close it
        if (!isBinOpen)
            TriggerOpenAnimation();
        else
            TriggerCloseAnimation();
    }

    public virtual void StopChore()
    {
        isWorking = false;
        OnChoreStopped?.Invoke();
    }
    
    public virtual void CompleteChore()
    {
        isWorking = false;
        OnChoreCompleted?.Invoke();

        isBinOpen = !isBinOpen;
        Debug.Log($"CompleteChore called. Bin is now {(isBinOpen ? "OPEN" : "CLOSED")}");

        // Only reset triggers if bin is now closed
        if (!isBinOpen && animator != null)
        {
            animator.ResetTrigger(openTrigger);
            animator.ResetTrigger(closeTrigger);
        }
    }
    public void Interact()
    {
        StartChore();
    }

    public void TriggerOpenAnimation()
    {
        Debug.Log("Triggering OpenBin animation");
        if (animator != null && !string.IsNullOrEmpty(openTrigger))
            animator.SetTrigger(openTrigger);
    }

    public void TriggerCloseAnimation()
    {
        Debug.Log("Triggering CloseBin animation");
        if (animator != null && !string.IsNullOrEmpty(closeTrigger))
            animator.SetTrigger(closeTrigger);
    }
}