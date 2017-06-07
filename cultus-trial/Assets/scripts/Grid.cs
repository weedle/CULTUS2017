﻿using UnityEngine;
using System;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
    public bool isVirtual = false;
    Cell[,] gridLayout = new Cell[5, 10];


	public Grid() {
		Console.Write("Huh, so you made a new grid, I think...");
	}

	// NOTE: the current grid layout is hard-coded for now =.=
	public void makeGrid() {
        gridLayout = new Cell[5,10];
        // NOTE: all cells in this grid uses '2-tiles' ( refer to labels in 'sprites' folder )
        for (int r = 0; r < gridLayout.GetLength(0); r++) {
			float xVal = r * -0.5f;
			float yVal = r * -0.25f;
			for (int i = 0; i < gridLayout.GetLength(1); i++) {
				//print("current pos values: x = " + xVal + " , y = " + yVal);
				Cell currentCell = new Cell (1, new Vector3(xVal, yVal), r, i);
                gridLayout[r,i] = currentCell;
				xVal += 0.5f;
				yVal += -0.25f;
			}
		}
	}
		

	public void loadGrid() {

		for (int r = gridLayout.GetLength(0) - 1; r >= 0; r--) {
			for (int i = gridLayout.GetLength(1) - 1; i >= 0; i--) {
                GameObject thisTile = new GameObject();
                gridLayout[r, i].cellObject = thisTile;
                SpriteRenderer spriter = thisTile.AddComponent<SpriteRenderer>();
                spriter.sprite = (Sprite)Resources.Load<Sprite>("sprites/01");
                thisTile.transform.position = gridLayout[r,i].getPos() - (r + i) * new Vector3(0,0,0.0001f);
                thisTile.name = "tile(" + r + "," + i + ")";
                if (isVirtual)
                {
                    spriter.sprite = (Sprite)Resources.Load<Sprite>("sprites/movementMarker");
                    spriter.sortingOrder = 1;
                    thisTile.name = "tileMovementMarker(" + r + "," + i + ")";
                    spriter.enabled = false;
                }
                //Instantiate (thisTile);
			}
		}
	}

    public void hideAll()
    {

        for (int r = gridLayout.GetLength(0) - 1; r >= 0; r--)
        {
            for (int i = gridLayout.GetLength(1) - 1; i >= 0; i--)
            {
                gridLayout[r, i].cellObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
    
	// RETURNS: a crude estimate of the center of this grid layout
	// USAGE: can use this to focus the camera on the centre cell
	// NOTE: there's a probably a better way of doing this !!!
	public Vector2 getCentrePos() {
		// calculate the middle row, 1st dimension
		int mid1 = (int) Math.Floor (gridLayout.GetLength(0) / 2.0);
        int mid2 = (int)Math.Floor(gridLayout.GetLength(1) / 2.0);
        return gridLayout[mid1, mid2].getPos(); 
	}


	public Cell[,] getLayout() {
		return gridLayout;
	}

	// USAGE: returns the n-th cell from current cell in the specified direction
	// NOTE: currently just used by the Unit class for movement process
	public Cell nextCell(Cell currentCell, Unit.Direction dir, int n) {

		int row = currentCell.getRow ();
		int col = currentCell.getCol ();
		Cell nthCell = currentCell;			// returns currentCell if all else fails
		int val = 0;

		switch (dir)
        {
            case Unit.Direction.LLeft:
                val = Mathf.Min (gridLayout.GetLength(0) - 1, row + n);
                nthCell = gridLayout [val,col];
                break;
            case Unit.Direction.URight:
				val = Mathf.Max(0, row - n);
                nthCell = gridLayout [val,col];
                break;

            case Unit.Direction.ULeft:
				val = Mathf.Max(0, col - n);
                nthCell = gridLayout [row,val];
                break;
            case Unit.Direction.LRight:
                val = Mathf.Min (gridLayout.GetLength(1) - 1, col + n);
                nthCell = gridLayout [row,val];
                break;
        }

		return nthCell;
	}

    public HashSet<Cell> getCellsWithinRange(Cell startingPos, int n)
    {
        HashSet<Cell> returnSet = addCellsWithinRangeRecursive(startingPos, n, new HashSet<Cell>());
        return returnSet;
    }

    private HashSet<Cell> addCellsWithinRangeRecursive(Cell pos, int n, HashSet<Cell> cells)
    {
        if (n == 0)
            return cells;
        Cell lleft = nextCell(pos, Unit.Direction.LLeft, 1);
        Cell lright = nextCell(pos, Unit.Direction.LRight, 1);
        Cell uleft = nextCell(pos, Unit.Direction.ULeft, 1);
        Cell uright = nextCell(pos, Unit.Direction.URight, 1);

        HashSet<Cell> neighbours = new HashSet<Cell>();
        neighbours.Add(lleft);
        neighbours.Add(lright);
        neighbours.Add(uleft);
        neighbours.Add(uright);
        cells.Add(pos);
        foreach (Cell cell in neighbours)
        {
            cells.Add(cell);
            cells.UnionWith(addCellsWithinRangeRecursive(cell, n - 1, cells));
        }
        return cells;
    }
}
