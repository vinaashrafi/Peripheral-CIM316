using UnityEngine;
using UnityEngine.EventSystems;

public class ComputerIcon : MonoBehaviour, IPointerClickHandler
{
    public int windowNumber;
    public void OnPointerClick(PointerEventData eventData)
    {
        ComputerManager.Current.OpenComputerWindow(windowNumber);
    }
}
