using UnityEngine;
using DG.Tweening;

public class DoorSwing : MonoBehaviour, IInteractable
{
    public bool isOpen = false;
    public float swingAngle = 90f;
    public float swingDuration = 2f;
    public Vector3 hingeAxis = Vector3.up;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.AngleAxis(swingAngle, hingeAxis) * closedRotation;
    }

    public void Interact()
    {
        ToggleDoor();
    }

    private void ToggleDoor()
    {
        // this is kinda ass and doesnt really work
        
        isOpen = !isOpen;
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;

        // Kill any existing tweens on this transform to avoid overlap
        transform.DOKill();
        transform.DORotateQuaternion(targetRotation, swingDuration)
            .SetEase(Ease.OutCubic);
    }

}