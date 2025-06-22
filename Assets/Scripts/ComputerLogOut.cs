using UnityEngine;
using UnityEngine.EventSystems;

public class ComputerLogOut : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private computer computerScript;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Logout button clicked!");
        if (computerScript != null)
        {
            computerScript.ExitComputer();
        }
        else
        {
            Debug.LogWarning("Computer script reference is missing.");
        }
    }
}