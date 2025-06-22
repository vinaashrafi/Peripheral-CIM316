using UnityEngine;

public class Printer : MonoBehaviour
{
    [SerializeField] private Animator printerAnimator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip printCompleteClip;

    // [SerializeField] private Collider printedItemCollider; // <- reference to child collider
 

    private void Start()
    {
        if (printerAnimator == null)
            printerAnimator = GetComponent<Animator>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        //
        // if (printedItemCollider != null)
        //     printedItemCollider.enabled = false; // make sure it's off initially
    }

    public void OnPrinted()
    {
        Debug.Log("Print complete. Playing animation and sound.");

        if (printerAnimator != null)
            printerAnimator.SetBool("OnPrinted", true);

        if (audioSource != null && printCompleteClip != null)
            audioSource.PlayOneShot(printCompleteClip);
        
    }
    
    // public void EnablePrintedItemCollider()
    // {
    //     if (printedItemCollider != null)
    //     {
    //         printedItemCollider.enabled = true;
    //         Debug.Log("Collider enabled via animation event.");
    //     }
    //     else
    //     {
    //         Debug.LogWarning("Printed item collider not assigned.");
    //     }
    //     printerAnimator.enabled = false;  //
    // }
    
 
    
}