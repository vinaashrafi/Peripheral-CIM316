using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinesweeperCell : MonoBehaviour, IPointerClickHandler
{
    public int Gridx;
    public int Gridy;
    
    public Image image;

    public bool flagged;

    public void NewNumberForCell(int x,int y)
    {
        Gridx = x;
        Gridy = y;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(Gridx + "," + Gridy);
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightClick();
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            LeftClick();
        }
    }

    public void LeftClick()
    {
        if (flagged) return;
        MinesweeperManager.Current.ThisCellClicked(Gridx,Gridy);
    }

    public void ResetFlag()
    {
        flagged = false;
    }

    public void RightClick()
    {
        flagged = !flagged;
        if (flagged)
        {
            MinesweeperManager.Current.ThisCellFlagged(Gridx,Gridy);
        }
        else
        {
            MinesweeperManager.Current.ThisCellUnflagged(Gridx,Gridy);
        }
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    private void OnEnable()
    {
        MinesweeperManager.Current.GameLostAction += ResetFlag;
    }

    private void OnDisable()
    {
        MinesweeperManager.Current.GameLostAction -= ResetFlag;
    }
}
