using UnityEngine;

public class PickUp : MonoBehaviour, IPickupable
{
    public void Pickup(Transform handTransform)
    {
        AttachToHand(handTransform);
        // Destroy(gameObject);
    }

    private void AttachToHand(Transform handTransform)
    {
        transform.SetParent(handTransform);
        transform.localPosition = new Vector3(0f, 0f, -15f); // Adjust as necessary
        transform.localRotation = Quaternion.identity; // Adjust as necessary
    }
}