using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FridgeController : ChoreBase
{
    public Transform leftDoorPivot;
    public Transform rightDoorPivot;

    public bool useRightDoor = false; // Checkbox to flip direction
    public float openAngleY = 160f;
    public float rotateDuration = 1.5f;
    public float autoReturnTime = 120f;

    private Quaternion leftClosedRotation;
    private Quaternion leftOpenRotation;
    private Quaternion rightClosedRotation;
    private Quaternion rightOpenRotation;

    private bool isAtClosedRotation = true;
    private Coroutine autoReturnCoroutine;

    void Start()
    {
        if (leftDoorPivot != null)
        {
            leftClosedRotation = leftDoorPivot.localRotation;
            leftOpenRotation = Quaternion.Euler(0, openAngleY, 0);
        }

        if (rightDoorPivot != null)
        {
            rightClosedRotation = rightDoorPivot.localRotation;
            rightOpenRotation = Quaternion.Euler(0, -openAngleY, 0); // Negative for opposite swing
        }
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

        ToggleFridgeDoors();

        if (autoReturnCoroutine != null)
            StopCoroutine(autoReturnCoroutine);

        autoReturnCoroutine = StartCoroutine(AutoReturnAfterDelay());
    }

    private void ToggleFridgeDoors()
    {
        isAtClosedRotation = !isAtClosedRotation;

        if (leftDoorPivot != null)
        {
            Quaternion targetRotation = isAtClosedRotation ? leftClosedRotation : leftOpenRotation;
            leftDoorPivot.DOKill();
            leftDoorPivot.DOLocalRotateQuaternion(targetRotation, rotateDuration)
                .SetEase(Ease.OutCubic);
        }

        if (rightDoorPivot != null && useRightDoor)
        {
            Quaternion targetRotation = isAtClosedRotation ? rightClosedRotation : rightOpenRotation;
            rightDoorPivot.DOKill();
            rightDoorPivot.DOLocalRotateQuaternion(targetRotation, rotateDuration)
                .SetEase(Ease.OutCubic);
        }

        Debug.Log($"Fridge door toggled. Now {(isAtClosedRotation ? "closed" : "open")}");
    }

    private IEnumerator AutoReturnAfterDelay()
    {
        yield return new WaitForSeconds(autoReturnTime);

        Debug.Log("Auto return triggered");
        ToggleFridgeDoors();

        autoReturnCoroutine = null;
    }
}