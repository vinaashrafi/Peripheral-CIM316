using UnityEngine;

public class DishChore : ChoreBase
{
    public override void CompleteChore()
    {
        base.CompleteChore();
        // Custom logic for dishes
        Destroy(gameObject); // Dish disappears
    }
}