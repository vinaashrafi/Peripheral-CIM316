using UnityEngine;
using UnityEngine.EventSystems;

public class ComputerLogOut : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private computer computerScript;
    
    private void Awake()
    {
        computerScript = FindObjectOfType<computer>();

        if (computerScript == null)
        {
            Debug.LogWarning("Computer script not found in scene.");
        }
    }
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