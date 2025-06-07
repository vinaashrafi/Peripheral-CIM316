using System;
using UnityEngine;

public abstract class ChoreBase : MonoBehaviour, IChoreable, IInteractable
{
    // public static event Action<ChoreBase> OnChoreCompleted; // Event for when a chore is completed

    [SerializeField] protected float timeToComplete = 3f; // Time to complete the chore
    protected float currentProgress = 0f; // Progress tracker
    protected bool isWorking = false; // Whether the chore is ongoing

    public event Action<float> OnChoreProgress; // Event to report progress

    
    public event Action OnChoreStarted;
    public event Action OnChoreStopped;
    
    public event Action OnChoreCompleted;
    
    
    
    
    public bool IsChoreActive()
    {
        return isWorking;
    }
    
    protected virtual void Update()
    {
        if (isWorking)
        {
            currentProgress += Time.deltaTime;
            OnChoreProgress?.Invoke(currentProgress / timeToComplete); // Normalize progress (0-1)

            if (currentProgress >= timeToComplete)
            {
                CompleteChore();
            }
        }
    }

    public virtual void StartChore()
    {
        isWorking = true;
        OnChoreStarted?.Invoke();
    }

    public virtual void StopChore()
    {
        isWorking = false;
        OnChoreStopped?.Invoke();
    }

    // protected virtual void CompleteChore()
    // {
    //     isWorking = false;
    //     OnChoreCompleted?.Invoke(this);
    //     Debug.Log($"{gameObject.name} chore completed!");
    // }

    protected virtual void CompleteChore()
    {
        isWorking = false;
        OnChoreCompleted?.Invoke(); // Local instance event
        Debug.Log($"{gameObject.name} chore completed!");
    }
    public void Interact()
    {
        StartChore();
    }
}