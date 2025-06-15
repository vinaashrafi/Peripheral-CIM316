using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image image;
    public Text text;
    
    public Color selectedColour, notSelectedColour;
    //public InventoryManager inventoryManager;
    private void Awake()
    {
        image = GetComponent<Image>();
        Deselect();
    }

    public void Select()
    {
        image.color = selectedColour;
        if (GetComponentInChildren<PickupItem>() != null)
        {
            GetComponentInChildren<PickupItem>().Selected();
        }
    }

    public void Deselect()
    {
        image.color = notSelectedColour;
        if (GetComponentInChildren<PickupItem>() != null)
        {
            GetComponentInChildren<PickupItem>().Deselected();
        }
    }
    /*
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        if (transform.childCount == 0)
        {
            draggableItem.parentAfterDrag = transform;
            return;
        }

        DraggableItem currentItem = GetComponentInChildren<DraggableItem>();
        if (transform.childCount != 0 && draggableItem.item == currentItem.item && (currentItem.count + draggableItem.count) <= InventoryManager.Current.stackMax)
        {
            currentItem.count += draggableItem.count;
            Destroy(draggableItem.gameObject);
            currentItem.RefreshCount();
            return;
        }
        if (transform.childCount != 0 && draggableItem.item == currentItem.item && (currentItem.count + draggableItem.count) > InventoryManager.Current.stackMax)
        {
            int stackRemainer = InventoryManager.Current.stackMax - currentItem.count;
            draggableItem.count -= stackRemainer;
            currentItem.count += stackRemainer;
            currentItem.RefreshCount();
            draggableItem.RefreshCount();
        }
    }
    */
    public PickupItem ReturnItemInSlot()
    {
        PickupItem currentItem = GetComponentInChildren<PickupItem>();
        return currentItem;
    }
    
}
