using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    [SerializeField] private GameObject choreListUI;
    [SerializeField] private string choreListItemName = "Paper";
    [SerializeField] private Transform choreTextContainer; // parent object that holds chore text lines
    [SerializeField] private GameObject choreTextPrefab;   // prefab with a TMP_Text component

    [SerializeField] private List<string> choreSequence = new List<string> { "Bin", " Wash Dishes", "Feed Cat" };

    private int currentChoreIndex = 0;
    private List<TextMeshProUGUI> choreTexts = new List<TextMeshProUGUI>();

    private void Start()
    {
        if (choreSequence.Count == 0 || choreTextPrefab == null || choreTextContainer == null)
            return;

        // Only show current task at the start
        AddChoreLine(choreSequence[0], Color.green);
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

    public void OnChoreCompleted(string choreName)
    {
        if (currentChoreIndex >= choreSequence.Count) return;

        // If the current chore was completed
        if (choreSequence[currentChoreIndex] == choreName)
        {
            // Change current line to red
            choreTexts[currentChoreIndex].color = Color.red;
            choreTexts[currentChoreIndex].text = $"{choreName} - Completed";

            currentChoreIndex++;

            // Add next task in green
            if (currentChoreIndex < choreSequence.Count)
            {
                AddChoreLine(choreSequence[currentChoreIndex], Color.green);
            }
        }
    }

    private void AddChoreLine(string text, Color color)
    {
        GameObject newLine = Instantiate(choreTextPrefab, choreTextContainer);
        TextMeshProUGUI textComponent = newLine.GetComponent<TextMeshProUGUI>();
        textComponent.text = text;
        textComponent.color = color;
        choreTexts.Add(textComponent);
    }
}