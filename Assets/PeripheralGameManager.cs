using TMPro;
using UnityEngine;

public class PeripheralGameManager : MonoBehaviour
{
    public static PeripheralGameManager Instance;

    [Header("Chore Tracking")]
    public int totalChores = 10;
    private int choresCompleted = 0;
    [SerializeField] private TextMeshProUGUI choreText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    private void OnEnable()
    {
        TaskEvents.OnChoreCompleted += HandleChoreComplete;
    }

    private void OnDisable()
    {
        TaskEvents.OnChoreCompleted -= HandleChoreComplete;
    }

    private void HandleChoreComplete()
    {
        choresCompleted++;
        Debug.Log($"Chores completed: {choresCompleted}/{totalChores}");

        if (choreText != null)
            choreText.text = $"Chores: {choresCompleted}/{totalChores}";

        if (choresCompleted >= totalChores)
        {
            Debug.Log("✅ All chores complete! Game over.");
            // Add win screen or ending logic here
        }
    }

}