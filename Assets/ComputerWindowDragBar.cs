using UnityEngine;
using UnityEngine.EventSystems;

public class ComputerWindowDragBar : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public GameObject computerWindow;
    public WindowTabBackground windowTabBackground;
    private Vector3 offset;
    public GameObject toolbar;
    public void OnDrag(PointerEventData eventData)
    {
        RectTransform rt = toolbar.GetComponent<RectTransform>();
        if(Input.mousePosition.x <= 0 || Input.mousePosition.y <= 0+rt.sizeDelta.y || Input.mousePosition.x >= Screen.width-1 || Input.mousePosition.y >= Screen.height-1)return;
        computerWindow.transform.position = Input.mousePosition + offset;
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = computerWindow.transform.position - Input.mousePosition;
        ComputerManager.Current.SetWindowIndexAsCurrentWindow(windowTabBackground.windowNumberIndex);
    }
}
