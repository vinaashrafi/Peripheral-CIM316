using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CurtainCOntroller : MonoBehaviour, IInteractable
{
    public bool slideOppositeDirection = false; // If true, slides backward (-Z)
    public float slideDistance = 1f;
    public float slideDuration = 2f;
    public float autoReturnTime = 120f; // 2 in-game minutes in seconds

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isAtClosedPosition = true;
    private Coroutine autoReturnCoroutine;

    void Start()
    {
        closedPosition = transform.localPosition;
        Vector3 direction = slideOppositeDirection ? -Vector3.forward : Vector3.forward;
        openPosition = closedPosition + direction * slideDistance;
    }

    public void Interact()
    {
        ToggleCurtain();

        // Restart auto-return timer
        if (autoReturnCoroutine != null)
            StopCoroutine(autoReturnCoroutine);
        autoReturnCoroutine = StartCoroutine(AutoReturnAfterDelay());
    }

    private void ToggleCurtain()
    {
        Vector3 targetPosition = isAtClosedPosition ? openPosition : closedPosition;

        transform.DOKill();
        transform.DOLocalMove(targetPosition, slideDuration)
            .SetEase(Ease.OutCubic);

        isAtClosedPosition = !isAtClosedPosition;

        Debug.Log($"{gameObject.name} sliding to {targetPosition}");
    }

    
    //reverts curtain back to original position 
    // Auto-moving curtains create a subtle loss of control and uncertainty, making the player question reality and heightening feelings of paranoia.
    private IEnumerator AutoReturnAfterDelay()
    {
        yield return new WaitForSeconds(autoReturnTime);

        Debug.Log($"{gameObject.name} auto-return triggered.");
        ToggleCurtain();

        autoReturnCoroutine = null;
    }
}