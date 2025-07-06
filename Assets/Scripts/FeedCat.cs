using UnityEngine;
using DG.Tweening;

public class FeedCat : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string requiredTag = "Player";
    [SerializeField] private string animatorBoolParameter = "EmptyCan"; // <- the bool you want to trigger
    [SerializeField] private SphereCollider rangeTrigger; // Auto-assigned if not manually set

    [SerializeField] private GameObject playerInRange = null;
    [SerializeField]  private Animator canAnimator = null;
    [SerializeField] private bool hasEmptied = false;

    private void Start()
    {
        if (rangeTrigger == null)
            rangeTrigger = GetComponent<SphereCollider>();

        if (rangeTrigger != null)
            rangeTrigger.isTrigger = true;
    }

    private void Update()
    {
        if (playerInRange != null && canAnimator != null && !hasEmptied)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Feeding cat...");
                canAnimator.SetBool(animatorBoolParameter, true);
                hasEmptied = true;

                // Optional: trigger event
                TaskEvents.InvokeChoreCompleted("Feed Cat");
            }
        }
    }

    public void SetHeldCanAnimator(Animator animator)
    {
        canAnimator = animator;
        hasEmptied = false; // reset if you pick up a new can
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is a cat food can (or has the canTag)
        if (other.CompareTag("CatFood"))
        {
            playerInRange = other.gameObject;
            Animator animator = other.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                canAnimator = animator;
                Debug.Log("Cat food can detected in bowl range.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CatFood"))
        {
            playerInRange = null;
            Animator animator = other.GetComponentInChildren<Animator>();
            if (animator == canAnimator)
            {
                canAnimator = null;
                hasEmptied = false; // reset when can leaves range
                Debug.Log("Cat food can left bowl range.");
            }
        }
    }
}