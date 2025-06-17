using UnityEngine;

public class Item : MonoBehaviour, IPickupable
{
    public ItemScriptable itemScriptable;
    public GameObject itemVisuals;
    public float dropForce = 1f;
    public void Pickup(Transform handTransform)
    {
        if(InventoryManager.Current.IsInventoryFull())return;
        transform.SetParent(handTransform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().isTrigger = true;
        PickupItem(itemScriptable);
    }

    public void Drop(Transform handTransform)
    {
        InventoryManager.Current.RemoveItem(itemScriptable);
        // Detach from hand
        transform.SetParent(null);
        // Reactivate physics
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        Collider col = GetComponent<Collider>();
        col.isTrigger = false;
        // Position it in front of the player slightly
        transform.position = handTransform.position + handTransform.forward * 1f;
        // Throw forward
        rb.AddForce(handTransform.forward * dropForce, ForceMode.Impulse);
        
    }


    public void PickupItem(ItemScriptable thisItemsScriptableObject)
    {
        InventoryManager.Current.AddItem(thisItemsScriptableObject, gameObject);
    }
    
}
