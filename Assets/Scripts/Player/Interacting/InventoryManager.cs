using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject ItemPrefab;
    public int stackMax = 8;

    public int selectedSlot = -1;
    
    public bool inventoryOpen = false;

    private static InventoryManager _current;
    public static InventoryManager Current { get { return _current; } }

    private void Awake()
    {
        if (_current != null && _current != this)
        {
            Destroy(this.gameObject);
        } else {
            _current = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        ChangeSelectedSlot(0);
        
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                if (selectedSlot == 4)
                {
                    ChangeSelectedSlot(0);
                }
                else
                {
                    ChangeSelectedSlot(selectedSlot + 1);
                }
                
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                if (selectedSlot == 0)
                {
                    ChangeSelectedSlot(4);
                }
                else
                {
                    ChangeSelectedSlot(selectedSlot - 1);
                }
                
            }
        }
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 6)
            {
                ChangeSelectedSlot(number-1);
            }
            else if(isNumber && number > 6 && number < 0)
            {
                inventorySlots[number - 1].Deselect();
                selectedSlot = -1;
                ChangedSlotEvent();
            }
        }
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (inventoryOpen) return;
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
       //ChangedSlotEvent();
    }

    public void UpdatedSelectedSlot()
    {
        //ChangedSlotEvent();
    }

    public delegate void ChangeSlotAction();
    public static event ChangeSlotAction ChangedSlotEvent;
    public GameObject ReturnItemEquiped()
    {
        PickupItem item = inventorySlots[selectedSlot].ReturnItemInSlot();
        if (item == null)
        {
            Debug.Log("Trying to update the equipped Item but there was no child on the selected slot, or something went wrong you big fucking NONCE!");
            return null;
        }
        return item.transform.gameObject;
    }
    public void AddItem(ItemScriptable item, GameObject itemObject)
    {
        GameObject itemGameObject = ReturnItemEquiped();
        if (itemGameObject == null)
        {
            SpawnNewItem(item, inventorySlots[selectedSlot], itemObject);
            return;
        }
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            PickupItem itemInSlot = slot.GetComponentInChildren<PickupItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < stackMax)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return;
            }
        }
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            PickupItem itemInSlot = slot.GetComponentInChildren<PickupItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot, itemObject);
                return;
            }
        }
    }

    void SpawnNewItem(ItemScriptable item, InventorySlot slot, GameObject itemObject)
    {
        GameObject newItemGo = Instantiate(ItemPrefab, slot.transform);
        PickupItem invItem = newItemGo.GetComponent<PickupItem>();
        invItem.InitialiseItem(item);
        invItem.GetComponent<PickupItem>().itemObject = itemObject;
        UpdatedSelectedSlot();
    }

    public void OpenInv()
    {
        inventoryOpen = true;
        OnInvOpened();
    }

    public void CloseInv()
    {
        inventoryOpen = false;
        OnInvClosed();
    }
    public delegate void InvClosed();
    public static event InvClosed OnInvClosed;
    public delegate void InvOpened();
    public static event InvOpened OnInvOpened;
}
