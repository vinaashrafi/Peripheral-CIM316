using UnityEngine;

public class SkipNight : MonoBehaviour,IInteractable
{
    public void Interact()
    {
        SkipTheNight();
    }

    private void SkipTheNight()
    {
        GameManager.Current.SkipToNextDay();
    }
}
