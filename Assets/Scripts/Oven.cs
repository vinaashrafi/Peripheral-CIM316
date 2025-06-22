using DG.Tweening;
using UnityEngine;

public class Oven : MonoBehaviour, IInteractable
{
    [Header("Door Settings")]
    public bool isOpen = false;
    public float swingAngle = 90f;
    public float swingDuration = 2f;

    [Header("Hinge Axis Selection")]
    public bool useXAxis = false;
    public bool useYAxis = true;
    public bool useZAxis = false;

    private Vector3 closedEuler;
    private Vector3 openEuler;

    void Start()
    {
        closedEuler = transform.localEulerAngles;
        openEuler = closedEuler;

        if (useXAxis)
            openEuler.x += swingAngle;
        else if (useYAxis)
            openEuler.y += swingAngle;
        else if (useZAxis)
            openEuler.z += swingAngle;
        else
            Debug.LogWarning("No axis selected for door swing. Defaulting to Y axis.");
    }

    public void Interact()
    {
        ToggleDoor();
    }

    private void ToggleDoor()
    {
        isOpen = !isOpen;
        Vector3 targetEuler = isOpen ? openEuler : closedEuler;

        transform.DOKill();
        transform.DOLocalRotate(targetEuler, swingDuration).SetEase(Ease.OutCubic);
    }
}