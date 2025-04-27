using UnityEngine;
using UnityEngine.UI;  // For using the UI Slider

public class ChoreProgressBar : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;  // Drag the slider from the scene
    [SerializeField] private ChoreBase chore;  // Reference to the chore object

    // private void OnEnable()
    // {
    //     // Subscribe to the progress update event if a chore is assigned
    //     if (chore != null)
    //     {
    //         chore.OnChoreProgress += UpdateProgressBar;
    //     }
    // }
    //
    // private void OnDisable()
    // {
    //     if (chore != null)
    //     {
    //         chore.OnChoreProgress -= UpdateProgressBar;
    //     }
    // }

    // Update the progress bar based on the current progress
    private void UpdateProgressBar(float progress)
    {
        if (progressSlider != null)
        {
            progressSlider.value = progress; // Set the slider value
        }
    }
    public void SetChore(ChoreBase newChore)
    {
        if (chore != null)
        {
            chore.OnChoreProgress -= UpdateProgressBar;
        }

        chore = newChore;

        if (chore != null)
        {
            chore.OnChoreProgress += UpdateProgressBar;
        }
    }
}