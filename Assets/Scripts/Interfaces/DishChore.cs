using UnityEngine;

public class DishChore : ChoreBase
{
    // if sink is turned on dishes take 50% faster wash time
    
    [SerializeField] private float baseWashTime = 5f;

    public override void StartChore()
    {
        // Adjust time based on sink status
        timeToComplete = Sink.IsSinkOn ? baseWashTime * 0.5f : baseWashTime;

        Debug.Log("Dish chore started. Time to complete: " + timeToComplete + "s");

        base.StartChore(); // Continue normal chore behaviour
    }

    public override void CompleteChore()
    {
        base.CompleteChore();

        Debug.Log("Dish chore completed.");
        Destroy(gameObject);
    }
}