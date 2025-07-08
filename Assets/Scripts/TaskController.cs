using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    [SerializeField] private GameObject choreListUI;
    [SerializeField] private string choreListItemName = "Paper";
    [SerializeField] private Transform choreTextContainer; // parent object that holds chore text lines
    [SerializeField] private GameObject choreTextPrefab;   // prefab with a TMP_Text component

    [SerializeField] private List<string> choreSequence = new List<string> { "Take out the rubbish", "Wash Dishes", "Feed Cat" };

    [SerializeField]  private List<TextMeshProUGUI> choreTexts = new List<TextMeshProUGUI>();
    [SerializeField]   private HashSet<string> completedChores = new HashSet<string>();

    public Color greenColour;
    public Color redColour;

    private void Awake()
    {
        if (choreListUI == null)
        {
            choreListUI = GameObject.Find("TaskListUI");
            if (choreListUI == null)
                Debug.LogWarning("choreListUI could not be found in the scene by name.");
        }

        if (choreTextPrefab == null)
        {
            GameObject prefabGO = GameObject.Find("TaskLine");
            if (prefabGO != null)
                choreTextPrefab = prefabGO;
            else
                Debug.LogWarning("choreTextPrefab not assigned and 'Task Line' GameObject not found.");
        }

        if (choreTextContainer == null)
        {
            GameObject containerGO = GameObject.Find("TaskParent");
            if (containerGO != null)
                choreTextContainer = containerGO.transform;
            else
                Debug.LogWarning("choreTextContainer not assigned and 'Task Parent' GameObject not found.");
        }
    }

    private void Start()
    {
        if (choreSequence.Count == 0 || choreTextPrefab == null || choreTextContainer == null)
            return;

        // Start by showing only the first chore
        AddChoreLine(choreSequence[0], greenColour);
    }

    private void Update()
    {
        GameObject itemGO = null;

        if (InventoryManager.Current != null)
        {
            itemGO = InventoryManager.Current.ReturnSelectedItemInInventory();
        }

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
        choreName = choreName.Trim();

        // Ignore if already completed
        if (completedChores.Contains(choreName))
            return;

        completedChores.Add(choreName);

        // Update existing chore line UI if visible
        for (int i = 0; i < choreTexts.Count; i++)
        {
            string existingText = choreSequence[i].Trim();
            if (string.Equals(existingText, choreName, StringComparison.OrdinalIgnoreCase))
            {
                choreTexts[i].color = redColour;
                choreTexts[i].text = $"<s>{existingText}</s>";
            }
        }

        // Reveal the next chore in the sequence, regardless of order completed
        if (choreTexts.Count < choreSequence.Count)
        {
            string nextChore = choreSequence[choreTexts.Count].Trim();

            if (completedChores.Contains(nextChore))
            {
                AddChoreLine($"<s>{nextChore}</s>", redColour);
            }
            else
            {
                AddChoreLine(nextChore, greenColour);
            }
        }
    }

    public int GetChoreCount()
    {
        return choreSequence.Count;
    }

    public int GetCompletedChoreCount()
    {
        return completedChores.Count;
    }

    private void AddChoreLine(string text, Color color)
    {
        Debug.Log("is line being added");
        GameObject newLine = Instantiate(choreTextPrefab, choreTextContainer);
        TextMeshProUGUI textComponent = newLine.GetComponent<TextMeshProUGUI>();
        textComponent.text = text;
        textComponent.color = color;
        choreTexts.Add(textComponent);
    }
}