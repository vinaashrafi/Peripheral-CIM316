using UnityEngine;


public static class TaskEvents
{
    public delegate void ChoreCompletedHandler();
    public static event ChoreCompletedHandler OnChoreCompleted;

    public static void InvokeChoreCompleted()
    {
        OnChoreCompleted?.Invoke();
        Debug.Log("📢 ChoreCompleted event invoked.");
    
    }
}