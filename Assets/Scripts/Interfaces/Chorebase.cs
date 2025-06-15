using System;
using UnityEngine;

public abstract class ChoreBase : MonoBehaviour, IChoreable, IInteractable
{
    public float timeToComplete = 3f;
    public float currentProgress = 0f;
    public bool isWorking = false;

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
    }

    public virtual void  Interact()
    {
        StartChore();
    }
}