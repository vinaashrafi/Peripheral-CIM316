
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject mainInventoryGroup;

    public PlayerMovement playerMovement;

    public GameObject itemEquipped;

    public GameObject fishingRod;
    
    public Transform handTransform; // Assign in Inspector
    private GameObject equippedItem;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        if (playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }
        
    }

    private void OnEnable()
    {
        // InventoryManager.ChangedSlotEvent += ChangeItemEquipped;
    }

    private void OnDisable()
    {
        // InventoryManager.ChangedSlotEvent -= ChangeItemEquipped;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Debug.Log("trying to open or close inv");
            OpenInventory();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Trying to Use an Item");
            // IUsable itemToUse = fishingRod.GetComponent<IUsable>();
            // itemToUse.UseObject();
            TryPickUp();
        }
        
    }

    // public void ChangeItemEquipped()
    // {
    //     // GameObject itemToEquip = InventoryManager.Current.ReturnItemEquiped();
    //     if (itemToEquip == null)
    //     {
    //         //GOTTEM
    //         itemEquipped = null;
    //         fishingRod.SetActive(false);
    //         return;
    //     }
    //     itemEquipped = InventoryManager.Current.ReturnItemEquiped();
    //     if (itemEquipped.GetComponent<DraggableItem>().ReturnItemTypeIndex() == 0)
    //     {
    //         fishingRod.SetActive(true);
    //     }
    //     else
    //     {
    //         fishingRod.SetActive(false);
    //     }
    // }

    void OpenInventory()
    {
        Debug.Log("Setting it to the other state");
        mainInventoryGroup.SetActive(!mainInventoryGroup.activeSelf);
        if (mainInventoryGroup.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            playerMovement.LockLookingAround();
        }
        if (!mainInventoryGroup.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerMovement.UnlockLookingAround();
        }
    }
    
    // pickups
    void TryPickUp()
    {
        Collider[] items = Physics.OverlapSphere(transform.position, 2f);
        foreach (var item in items)
        {
            if (item.CompareTag("PickUp"))
            {
                PickUp(item.gameObject);
                return;
            }
        }
    }

    void PickUp(GameObject item)
    {
        equippedItem = item;
        equippedItem.transform.SetParent(handTransform);
        equippedItem.transform.localPosition = Vector3.zero;
        equippedItem.transform.localRotation = Quaternion.identity;
    
        Rigidbody rb = equippedItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        
    }
    
    void Drop(GameObject item)
    {
        item = null;
        equippedItem = item;
        equippedItem.transform.SetParent(handTransform);
        equippedItem.transform.localPosition = Vector3.zero;
        equippedItem.transform.localRotation = Quaternion.identity;
    
        Rigidbody rb = equippedItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        
    }


}
