using UnityEngine;

public class GenericPickUpBase : MonoBehaviour, IPickupable
{
    //inherit from this class, to use this script
    public void Pickup(Transform handTransform)
    {
        transform.SetParent(handTransform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic = true;
        // Maybe disable collider, or trigger "held" animation
    }
}
