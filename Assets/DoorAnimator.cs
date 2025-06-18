using UnityEngine;

public class DoorAnimator : MonoBehaviour,IInteractable
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool isOpen = false;

    [SerializeField]  private Collider collisionCollider;
    [SerializeField] private Collider interactionTrigger;

    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        // Separate the blocking collider and trigger collider
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider col in colliders)
        {
            if (col.isTrigger)
                interactionTrigger = col;
            else
                collisionCollider = col;
        }

        if (animator == null)
            Debug.LogWarning("Animator component missing on Door!");

        if (collisionCollider == null)
            Debug.LogWarning("Blocking collider not found!");

        if (interactionTrigger == null)
            Debug.LogWarning("Interaction trigger not found!");
    }

    public void Interact()
    {
        ToggleDoor();
    }

    private void ToggleDoor()
    {
        isOpen = !isOpen;
        animator.SetBool("Open", isOpen);

        if (collisionCollider != null)
            collisionCollider.enabled = !isOpen; // disable when open, enable when closed

        Debug.Log("Door is now " + (isOpen ? "open" : "closed"));
    }
}