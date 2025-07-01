using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CurtainCOntroller : ChoreBase
{
    [Header("Slide Axis")]
    public bool slideX = false;
    public bool slideY = false;
    public bool slideZ = true;

    [Header("Slide Settings")]
    public bool slideOppositeDirection = false;
    public float slideDistance = 1f;
    public float slideDuration = 2f;

    [Header("Auto Return Settings")]
    public bool enableAutoReturn = true;
    public float autoReturnTime = 120f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isAtClosedPosition = true;
    private Coroutine autoReturnCoroutine;

    void Start()
    {
        closedPosition = transform.localPosition;

        Vector3 direction = Vector3.zero;

        if (slideX) direction = slideOppositeDirection ? -Vector3.right : Vector3.right;
        else if (slideY) direction = slideOppositeDirection ? -Vector3.up : Vector3.up;
        else if (slideZ) direction = slideOppositeDirection ? -Vector3.forward : Vector3.forward;

        openPosition = closedPosition + direction * slideDistance;
    }

    public override void Interact()
    {
        base.Interact();

        if (autoReturnCoroutine != null)
        {
            StopCoroutine(autoReturnCoroutine);
            autoReturnCoroutine = null;
        }
    }

    public override void CompleteChore()
    {
        base.CompleteChore();

        ToggleCurtain();

        if (autoReturnCoroutine != null)
            StopCoroutine(autoReturnCoroutine);

        if (enableAutoReturn)
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