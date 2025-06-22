using UnityEngine;
using UnityEngine.EventSystems;

public class ComputerPrintButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Printer printer;

    private void Awake()
    {
        if (printer == null && GameObject.FindObjectOfType<Printer>() != null)
        {
            printer = GameObject.FindObjectOfType<Printer>();
        }

        if (printer == null)
        {
            Debug.LogWarning("Printer script not found.");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (printer != null)
        {
            printer.OnPrinted();
        }
        else
        {
            Debug.LogWarning("Printer reference is missing.");
        }
    }
}