using UnityEngine;
using UnityEngine.EventSystems;

public class MinesweeperReset : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        MinesweeperManager.Current.ResetGame();
    }
}
