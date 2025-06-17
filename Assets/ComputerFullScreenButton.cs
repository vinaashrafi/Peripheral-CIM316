using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ComputerFullScreenButton : MonoBehaviour, IPointerClickHandler
{
    public int windowNumber;
    public void OnPointerClick(PointerEventData eventData)
    {
        ComputerManager.Current.OpenComputerWindow(windowNumber);
    }
}
