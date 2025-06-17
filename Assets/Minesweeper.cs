using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Minesweeper : MonoBehaviour
{
    public int gridW;
    public int gridH;
    public int numMines;

    public int[][] mines;
    public bool[][] flags;
    public bool[][] revealed;
    
    public MinesweeperCell[] cells;
    public Vector2[] mineLocations;
    public Vector2[] revealLocations = new Vector2[99];
    public Vector2[] flagLocations = new Vector2[100];
    public int noRevealed;
    
    public Sprite[] sprites;

    public bool playerClicked00;

    public bool gameLost;
    private void Awake()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        playerClicked00 = false;
        has00beenchecked = false;
        revealLocations = new Vector2[0];
        revealLocations = new Vector2[300];
        flagLocations = new Vector2[0];
        flagLocations = new Vector2[100];
        AssignCells();
        AssignMines();
        gameLost = false;
        
    }
    public int NumberFromCoords(int x, int y)
    {
        int num = x * 10 + y;
        return num;
    }

    public void AssignMines()
    {
        for (int i = 0; i < numMines; i++)
        {
            int x = Random.Range(0, 9);
            int y = Random.Range(0, 9);
            mineLocations[i] = new Vector2(x, y);
        }
    }
    public void MineSweeperUpdate(int x, int y)
    {
        if(gameLost)return;
        if (x == 0 && y == 0 && has00beenchecked == true && playerClicked00 == false)
        {
            has00beenchecked = false;
            playerClicked00 = true;
        }
        RevealCells(x,y);
    }
    
    public void MinesweeperFlagged(int x, int y)
    {
        if(gameLost)return;
        cells[NumberFromCoords(x,y)].SetSprite(sprites[9]);
        flagLocations[NumberFromCoords(x,y)] = new Vector2(x, y);
    }
    public void MinesweeperUnflagged(int x, int y)
    {
        if (gameLost)return;
        cells[NumberFromCoords(x,y)].SetSprite(sprites[10]);
        flagLocations[NumberFromCoords(x,y)] = new Vector2(0, 0);
    }

    public bool IsLocationFlagged(int x, int y)
    {
        if (flagLocations[NumberFromCoords(x, y)] != Vector2.zero)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RevealCells(int x, int y)
    {
        if (IsCellOutOfRange(x, y)) {return;}
        if(IsLocationFlagged(x,y)){return;}
        if (IsCellRevealed(x,y)) {return;}
        revealLocations[noRevealed] = new Vector2(x, y);
        noRevealed++;
        if (isCellAMine(x, y))
        {
            gameLost = true;
            for (int i = 0; i < mineLocations.Length; i++)
            {
                cells[NumberFromCoords(Mathf.RoundToInt(mineLocations[i].x),Mathf.RoundToInt(mineLocations[i].y))].SetSprite(sprites[11]);
            }
            cells[NumberFromCoords(x,y)].SetSprite(sprites[12]);
            return;
            
        }
        else
        {
            cells[NumberFromCoords(x,y)].SetSprite(sprites[0]);
            Debug.Log("Sprite set as cleared" + sprites[0]);
        }

        int minesnearby = CalculateNeighbours(x, y);
        if (minesnearby != 0)
        {
            cells[NumberFromCoords(x,y)].SetSprite(sprites[minesnearby]);
            return;
        }
        for (int offsetX=-1; offsetX<=1; offsetX++) {
            for (int offsetY=-1; offsetY<=1; offsetY++) {
                RevealCells(offsetX+x,offsetY+y);
            }
        }
    }

    public bool isCellAMine(int x, int y)
    {
        Vector2 newCoords = new Vector2(x, y);
        for (int i = 0; i < mineLocations.Length; i++)
        {
            if (mineLocations[i] == newCoords)
            {
                return true;
            }
        }
        return false;
    }
    public int CalculateNeighbours(int x, int y)
    {
        int i=0;
        for (int offsetX=-1; offsetX<=1; offsetX++) {
            for (int offsetY=-1; offsetY<=1; offsetY++) {
                if (isCellAMine(offsetX + x, offsetY + y))
                {
                    i++;
                }
            }
        }
        return i;
    }

    public bool IsCellOutOfRange(int x, int y)
    {
        if (x < 0 || x > gridW || y < 0 || y > gridH)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool has00beenchecked = false;
    public bool IsCellRevealed(int x, int y)
    {
        Vector2 newCoords = new Vector2(x, y);
        for (int i = 0; i < revealLocations.Length; i++)
        {
            if (revealLocations[i] == newCoords)
            {
                if (has00beenchecked == false && !isCellAMine(x, y))
                {
                    has00beenchecked = true;
                    return false;
                }
                return true;
            }
        }
        return false;
    }
    
    
    public void AssignCells()
    {
        int i = 0;
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    YouareYou(i,x, y);
                    cells[i].SetSprite(sprites[10]);
                    i++;
                }
            }
    }
    public void YouareYou(int cellNo,int x,int y)
    {
        cells[cellNo].NewNumberForCell(x,y);
    }
}
