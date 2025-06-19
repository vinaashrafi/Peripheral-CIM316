using UnityEngine;

public class CardboardController : ChoreBase
{
    [SerializeField] private Animator cardboardAnimator;
    [SerializeField] private bool isOpen = false;

    public override void CompleteChore()
    {
        base.CompleteChore();

        if (cardboardAnimator != null)
        {
            if (!isOpen)
            {
                // Play open animation
                cardboardAnimator.SetBool("Open", true);
                cardboardAnimator.SetBool("Close", false);
                Debug.Log("Cardboard opened.");
            }
            else
            {
                // Play close animation
                cardboardAnimator.SetBool("Close", true);
                cardboardAnimator.SetBool("Open", false);
                Debug.Log("Cardboard closed.");
            }

            isOpen = !isOpen; // Toggle the state
        }
    }
}