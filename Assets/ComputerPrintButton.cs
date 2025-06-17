using UnityEngine;
using UnityEngine.EventSystems;

public class ComputerPrintButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject printer;
    public void OnPointerClick(PointerEventData eventData)
    {
        //printer.print();
    }
}
