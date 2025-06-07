using UnityEngine;
using UnityEngine.UI;  // For using the UI Slider

public class ChoreProgressBar : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;
    [SerializeField] private ChoreBase chore;

    private void Awake()
    {
        if (progressSlider != null)
            progressSlider.gameObject.SetActive(false); // start hidden
    }

    public void SetChore(ChoreBase newChore)
    {
        // Unsubscribe from old chore
        if (chore != null)
        {
            chore.OnChoreProgress -= UpdateProgressBar;
            chore.OnChoreStarted -= ShowSlider;
            chore.OnChoreStopped -= HideSlider;
            chore.OnChoreCompleted -= HideSlider;
        }

        chore = newChore;

        if (chore != null)
        {
            chore.OnChoreProgress += UpdateProgressBar;
            chore.OnChoreStarted += ShowSlider;
            chore.OnChoreStopped += HideSlider;
            chore.OnChoreCompleted += HideSlider;
        }
    }

    private void ShowSlider()
    {
        if (progressSlider != null)
        {
            progressSlider.value = 0f;
            progressSlider.gameObject.SetActive(true);
        }
    }

    private void HideSlider()
    {
        if (progressSlider != null)
            progressSlider.gameObject.SetActive(false);
    }

    private void UpdateProgressBar(float progress)
    {
        if (progressSlider != null)
            progressSlider.value = progress;
    }
}