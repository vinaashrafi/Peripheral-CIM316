using TMPro;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    [SerializeField] private GameObject choreListUI;
    [SerializeField] private string choreListItemName = "Paper"; // Match the ItemScriptable name
    [SerializeField] private TextMeshProUGUI choreUIText;
    [SerializeField] private string trackedTaskName = "Bin";  
    
    private void Start()
    {
        // Set initial color (optional)
        if (choreUIText != null)
            choreUIText.color = Color.green;
    }
    
    private void Update()
    {
        GameObject itemGO = InventoryManager.Current.ReturnSelectedItemInInventory();

        if (itemGO != null)
        {
            Item itemComponent = itemGO.GetComponent<Item>();

            if (itemComponent != null && itemComponent.itemScriptable != null &&
                itemComponent.itemScriptable.name == choreListItemName)
            {
                choreListUI.SetActive(true);
                return;
            }
        }

        choreListUI.SetActive(false);
    }
    
    
    // This method will be called from PeripheralGameManager when a chore completes
    public void OnChoreCompleted(string choreName)
    {
        if (choreName == trackedTaskName && choreUIText != null)
        {
            choreUIText.color = Color.red;
            choreUIText.text = $"{trackedTaskName} - Completed";
        }
    }
}
