using UnityEngine;
using UnityEngine.EventSystems;
public class ComputerExitButton : MonoBehaviour, IPointerClickHandler
{
    public ComputerIcon app;
    public void OnPointerClick(PointerEventData eventData)
    {
        ComputerManager.Current.CloseWindow(app.windowNumber);
    }
}
