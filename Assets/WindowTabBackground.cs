using UnityEngine;
using UnityEngine.EventSystems;

public class WindowTabBackground : MonoBehaviour,IPointerClickHandler
{
    public int windowNumberIndex;
    public void OnPointerClick(PointerEventData eventData)
    {
        ComputerManager.Current.SetWindowIndexAsCurrentWindow(windowNumberIndex);
    }
}
