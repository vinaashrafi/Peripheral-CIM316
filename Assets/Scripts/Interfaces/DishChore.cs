using UnityEngine;

public class DishChore : ChoreBase
{
    protected override void CompleteChore()
    {
        base.CompleteChore();
        // Custom logic for dishes
        Destroy(gameObject); // Dish disappears
    }
}