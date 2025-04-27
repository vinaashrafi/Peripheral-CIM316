using System;
using UnityEngine;

public abstract class ChoreBase : MonoBehaviour, IChoreable
{
    public static event Action<ChoreBase> OnChoreCompleted;

    [SerializeField] protected float timeToComplete = 3f;
    protected float currentProgress = 0f;
    protected bool isWorking = false;

    protected virtual void Update()
    {
        if (isWorking)
        {
            currentProgress += Time.deltaTime;
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