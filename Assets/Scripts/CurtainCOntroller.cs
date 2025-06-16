using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CurtainCOntroller : ChoreBase
{
    public bool slideOppositeDirection = false; // If true, slides backward (-Z)
    public float slideDistance = 1f;
    public float slideDuration = 2f;
    public float autoReturnTime = 120f; // 2 minutes

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

    // Override Interact to call base StartChore (which starts progress)
    public override void Interact()
    {
        base.Interact();  // calls StartChore()

        // If there's an auto-return coroutine running, stop it so curtain won't auto close during new chore
        if (autoReturnCoroutine != null)
        {
            StopCoroutine(autoReturnCoroutine);
            autoReturnCoroutine = null;
        }
    }

    // Override CompleteChore to toggle curtain once chore time is done
    public override void CompleteChore()
    {
        base.CompleteChore();

        ToggleCurtain();

        // Restart auto-return timer after sliding
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

    private IEnumerator AutoReturnAfterDelay()
    {
        yield return new WaitForSeconds(autoReturnTime);

        Debug.Log($"{gameObject.name} auto-return triggered.");
        ToggleCurtain();

        autoReturnCoroutine = null;
    }
}