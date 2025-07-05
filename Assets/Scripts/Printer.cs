using UnityEngine;

public class Printer : MonoBehaviour
{
    [SerializeField] private Animator printerAnimator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip printCompleteClip;
    [SerializeField] private bool hasPrinted = false; // Prevent spamming sound
    [SerializeField] private Collider printedItemCollider; // <- reference to child collider
 
    [SerializeField] private GameObject paperPrefab;       // Your paper prefab
    [SerializeField] private Transform paperSpawnPoint;     // Usually same as PaperAnimator
    private GameObject currentPaper;    
    
    
    private void Start()
    {
        if (printerAnimator == null)
            printerAnimator = GetComponent<Animator>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        
        if (printedItemCollider != null)
            printedItemCollider.enabled = false; // make sure it's off initially
    }

    public void OnPrinted()
    {
        if (hasPrinted)
            return;

        Debug.Log("Print complete. Playing animation and sound.");
        
        // Spawn new paper
        if (paperPrefab != null && paperSpawnPoint != null)
        {
            // Instantiate and parent under animated transform
            currentPaper = Instantiate(paperPrefab, paperSpawnPoint);
            currentPaper.transform.localPosition = Vector3.zero;
            currentPaper.transform.localRotation = Quaternion.identity;
        }

        if (printerAnimator != null)
        {
            printerAnimator.enabled = true; // Re-enable in case it was disabled
            printerAnimator.Rebind();       // Reset animator to default state
            printerAnimator.Update(0f);     // Apply the reset immediately
            printerAnimator.SetBool("OnPrinted", true); // Start animation
        }

        if (audioSource != null && printCompleteClip != null)
            audioSource.PlayOneShot(printCompleteClip);

        hasPrinted = true;
    }
    
    public void FinishPrint()
    {
        if (printerAnimator != null)
            printerAnimator.SetBool("OnPrinted", false);
        
        printerAnimator.enabled = false;
        
        if (printedItemCollider != null)
            printedItemCollider.enabled = true; // enable collider when printing is finished
        
        // Also enable collider on the instantiated paper itself
        if (currentPaper != null)
        {
            // Collider paperCollider = currentPaper.GetComponent<Collider>();
            // if (paperCollider != null)
            //     paperCollider.enabled = true;

            // If collider is on a child object:
            Collider paperCollider = currentPaper.GetComponentInChildren<Collider>();
            if (paperCollider != null) paperCollider.enabled = true;
        }

        
        
        hasPrinted = false;
        
    }
    

    
 
    
}