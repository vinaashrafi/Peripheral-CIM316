using System;
using UnityEngine;

public abstract class ChoreBase : MonoBehaviour, IChoreable
{
    public static event Action<ChoreBase> OnChoreCompleted; // Event for when a chore is completed

    [SerializeField] protected float timeToComplete = 3f; // Time to complete the chore
    protected float currentProgress = 0f; // Progress tracker
    protected bool isWorking = false; // Whether the chore is ongoing

    public event Action<float> OnChoreProgress; // Event to report progress

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
    }

    public virtual void StopChore()
    {
        isWorking = false;
    }

    protected virtual void CompleteChore()
    {
        isWorking = false;
        OnChoreCompleted?.Invoke(this);
        Debug.Log($"{gameObject.name} chore completed!");
    }
}