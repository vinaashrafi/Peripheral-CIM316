
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
}
