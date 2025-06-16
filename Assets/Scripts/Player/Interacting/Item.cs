using UnityEngine;

public class Item : MonoBehaviour, IPickupable
{
    public ItemScriptable itemScriptable;
    public GameObject itemVisuals;
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

    public void PickupItem(ItemScriptable thisItemsScriptableObject)
    {
        InventoryManager.Current.AddItem(thisItemsScriptableObject, gameObject);
    }
    
}
