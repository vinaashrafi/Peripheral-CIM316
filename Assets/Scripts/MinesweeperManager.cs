using System;
using UnityEngine;

public class MinesweeperManager : MonoBehaviour
{
    private static MinesweeperManager _current;
    public static MinesweeperManager Current { get { return _current; } }
    private void Awake()
    {
        if (_current != null && _current != this)
        {
            Destroy(this.gameObject);
        } else {
            _current = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public Minesweeper Minesweeper;
    public void ThisCellClicked(int x, int y)
    {
        Minesweeper.MineSweeperUpdate(x,y);
    }

    public void ThisCellFlagged(int x, int y)
    {
        Minesweeper.MinesweeperFlagged(x,y);
    }

    public void ThisCellUnflagged(int x, int y)
    {
        Minesweeper.MinesweeperUnflagged(x,y);
    }

    public Action GameLostAction;
    public void ResetGame()
    {
        GameLostAction?.Invoke();
        Minesweeper.ResetGame();
    }
}
