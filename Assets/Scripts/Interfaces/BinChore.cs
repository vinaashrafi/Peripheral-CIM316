using System.Collections;
using UnityEngine;

public class BinChore : ChoreBase
{
    public override void CompleteChore()
    {
        base.CompleteChore();  // Calls the base logic (toggles bin state etc.)

        // If you want, add any extra logic here, e.g. sound effects, UI updates
    }
}