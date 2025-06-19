using TMPro;
using UnityEngine;

public class PeripheralGameManager : MonoBehaviour
{
    public static PeripheralGameManager Instance;

    [Header("Chore Tracking")]
    [SerializeField] public int totalChores = 10;
    [SerializeField] private float choresCompleted = 0;
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
        Debug.Log("PeripheralGameManager subscribing to chore completed event.");
        TaskEvents.OnChoreCompleted += HandleChoreComplete;
    }

    private void OnDisable()
    {
        TaskEvents.OnChoreCompleted -= HandleChoreComplete;
    }
    private void HandleChoreComplete()
    {
      
        choresCompleted += 0.5f; // count half per call
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