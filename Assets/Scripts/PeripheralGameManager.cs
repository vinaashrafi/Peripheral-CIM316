using TMPro;
using UnityEngine;

public class PeripheralGameManager : MonoBehaviour
{
    public static PeripheralGameManager Instance;

    [SerializeField] private TextMeshProUGUI choreText;
    [SerializeField] private TaskController taskController; // Assign in inspector

    [SerializeField] private bool allChoresDone = false; // For inspector view, read-only
    
    
    public GameObject rain;
    public FPController _player;
    public FadeController fade;

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
    
    public FPController returnFPController()
    {
        return _player;
    }

    public void SetFPController(FPController player)
    {
        _player = player;
        
    }

    private void HandleChoreComplete(string taskName)
    {
        taskName = taskName.Trim();

        Debug.Log($"✅ Task completed: {taskName}");

        // Tell TaskController to update its UI state
        taskController?.OnChoreCompleted(taskName);

        // Update UI count based on TaskController's completed chores count
        int completedCount = taskController != null ? taskController.GetCompletedChoreCount() : 0;
        int totalChores = taskController != null ? taskController.GetChoreCount() : 0;

        // choreText.text = $"Chores: {completedCount}/{totalChores}";

        allChoresDone = (completedCount >= totalChores && totalChores > 0); 
        
        if (allChoresDone)
        {
            Debug.Log("🎉 All chores completed! GO TO SLEEP");
            StartSleep(); // Act on the flag being true
        }
    }

    public void RainStart()
    {
        rain.SetActive(true);
    }

    public void StartSleep()
    {
        fade.StartFadeIn();
        _player.DisableInput();
    }

    public void StartWakeUp()
    {
        fade.StartFadeOut();
    }
}