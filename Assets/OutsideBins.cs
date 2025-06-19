using DG.Tweening;
using UnityEngine;

public class OutsideBins : MonoBehaviour
{
    [SerializeField] private Transform bagDropPoint; // Position inside bin for bag to settle
    [SerializeField] private Oven binScript;         // Your bin open/close script
    [SerializeField] private SphereCollider binTriggerCollider;

    private bool lastIsOpenState;

    private void Start()
    {
        if (binScript == null)
            binScript = GetComponentInChildren<Oven>();

        if (binScript == null)
            Debug.LogWarning("Bin script not found on bin object!");

        binTriggerCollider = GetComponentInChildren<SphereCollider>();
        if (binTriggerCollider == null)
            Debug.LogWarning("SphereCollider trigger not found!");

        // Set collider initially according to bin open state
        lastIsOpenState = binScript != null && binScript.isOpen;
        if (binTriggerCollider != null)
            binTriggerCollider.enabled = lastIsOpenState;
    }

    private void Update()
    {
        if (binScript == null || binTriggerCollider == null) return;

        // Check if bin open state changed since last frame
        if (binScript.isOpen != lastIsOpenState)
        {
            binTriggerCollider.enabled = binScript.isOpen;
            lastIsOpenState = binScript.isOpen;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!binScript.isOpen) return;             // Only accept if bin is open
        if (!other.CompareTag("BinBag")) return;   // Only accept bin bags

        Item item = other.GetComponent<Item>();
        if (item == null || !item.hasBeenDropped) return;  // Only accept dropped bags

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null) return;

        rb.isKinematic = true;  // Stop physics for smooth move

        other.transform.DOMove(bagDropPoint.position, 1f).SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                Debug.Log("Bag settled inside bin.");

                // Keep kinematic so it stays put
                rb.isKinematic = true;

                // Notify inventory system that bag was deposited
                InventoryManager.Current.RemoveItem();
            });
    }
}