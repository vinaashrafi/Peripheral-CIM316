using UnityEngine;

public class CardboardController : ChoreBase
{
    [SerializeField] private Animator cardboardAnimator;
    [SerializeField] private bool isOpen = false;
    
    private Collider[] childColliders;

    private void Awake()
    {
        // Get all child colliders (excluding this object's own collider, if any)
        childColliders = GetComponentsInChildren<Collider>(includeInactive: true);

        // Disable all child colliders at the start
        foreach (Collider col in childColliders)
        {
            if (col.gameObject != this.gameObject)
                col.enabled = false;
        }
    }

    public override void CompleteChore()
    {
        base.CompleteChore();

        if (cardboardAnimator != null)
        {
            if (!isOpen)
            {
                // Open the box
                cardboardAnimator.SetBool("Open", true);
                cardboardAnimator.SetBool("Close", false);
                Debug.Log("Cardboard opened.");

                // Enable all child colliders
                foreach (Collider col in childColliders)
                {
                    if (col.gameObject != this.gameObject)
                        col.enabled = true;
                }
            }
            else
            {
                // Close the box
                cardboardAnimator.SetBool("Close", true);
                cardboardAnimator.SetBool("Open", false);
                Debug.Log("Cardboard closed.");

                // Disable all child colliders
                foreach (Collider col in childColliders)
                {
                    if (col.gameObject != this.gameObject)
                        col.enabled = false;
                }
            }

            isOpen = !isOpen; // Toggle state
        }
    }
}