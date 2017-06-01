using UnityEngine;
using System;


public class Grid : MonoBehaviour {
	
	Cell[][] gridLayout = new Cell[3][];


	public Grid() {
		Console.Write("Huh, so you made a new grid, I think...");
	}

	// might contain more variables later
	public struct Cell {
		private bool isOccupied;
		int tileType;
		Vector3 currentPos;
		Unit currentUnit;
		private int row;
		private int col;

		// NOTE: a NULL UNIT is assigned to Cell upon construction
		public Cell(int type, Vector3 position, int row, int col){
			tileType = type;
			isOccupied = false;
			currentPos = position;
			currentUnit = null;
			this.row = row;
			this.col = col;
		}

		public int getTileType() {
			return tileType;
		}
		public Vector3 getPos() {
			return currentPos;
		}
		public void setPos(Vector3 position) {
			currentPos = position;
		}
		public bool getOccupied() {
			return isOccupied;
		}
		public int getRow() {
			return row;
		}
		public int getCol() {
			return col;
		}

		// USAGE: use within the process of emptying a cell of
		//			its unit
		// WARNING: unoccupy() will only proceed if this cell
		//				is actually occupied
		// NOTE: might want to revisit this method at a later phase
		public void unoccupy() {
			if (isOccupied) {
				isOccupied = !isOccupied;
				currentUnit = null;
			}
		
		}

		// USAGE: when you want to set a new unit to an
		//			EMPTY cell; changes will not proceed if
		//			current cell is occupied
		// NOTE: might want to revisit this method at a later phase
		public void setUnit(Unit unit) {
			if (!isOccupied) {
				currentUnit = unit;
				isOccupied = true;
			}
		}
			
	}



	// NOTE: the current grid layout is hard-coded for now =.=
	public void makeGrid() {

		gridLayout [0] = new Cell[5];
		gridLayout [1] = new Cell[4];
		gridLayout [2] = new Cell[3];

		// NOTE: all cells in this grid uses '2-tiles' ( refer to labels in 'sprites' folder )
		for (int r = 0; r < gridLayout.Length; r++) {
			Cell[] currentRow = gridLayout [r];
			float xVal = r * -0.5f;
			float yVal = r * -0.25f;
				
			for (int i = 0; i < currentRow.Length; i++) {
				Console.Write ("current pos values: x = " + xVal + " , y = " + yVal);
				Cell currentCell = new Cell (1, new Vector3(xVal, yVal), r, i);
				currentRow [i] = currentCell;
				xVal += 0.5f;
				yVal += -0.25f;
			}
		}
	}
		

	public void loadGrid() {

		for (int r = gridLayout.Length-1; r >= 0; r--) {
			Cell[] currentRow = gridLayout [r];

			for (int i = currentRow.Length-1; i >= 0; i--) {
				GameObject thisTile = (GameObject) Resources.Load ("1");
				thisTile.transform.position = currentRow [i].getPos();
				thisTile.name = "tile" + r + i;
				Instantiate (thisTile);
			}
		}
	}
		


	// RETURNS: a crude estimate of the center of this grid layout
	// USAGE: can use this to focus the camera on the centre cell
	// NOTE: there's a probably a better way of doing this !!!
	public Vector2 getCentrePos() {
		// calculate the middle row, 1st dimension
		int mid = (int) Math.Floor (gridLayout.GetLength (0) / 2.0);
		Cell[] midRow = gridLayout [mid];

		// calculate the middle cell of the middle row
		mid = (int) Math.Floor(midRow.Length / 2.0);
		return midRow [mid].getPos (); 
	}


	public Cell[][] getLayout() {
		return gridLayout;
	}

	// USAGE: returns the n-th cell from current cell in the specified direction
	// NOTE: currently just used by the Unit class for movement process
	public Cell nextCell(Cell currentCell, Unit.Direction dir, int n) {

		int row = currentCell.getRow ();
		int col = currentCell.getCol ();
		Cell nthCell = currentCell;			// returns currentCell if all else fails
		int min = 0;

		switch (dir) {
		case Unit.Direction.LLeft:
			min = Mathf.Min (gridLayout.GetLength (0) - 1, row + n);
			nthCell = gridLayout [min] [col];
			break;
		case Unit.Direction.URight:
			min = Mathf.Min (0, row - n);
			nthCell = gridLayout [min] [col];
			break;

		case Unit.Direction.ULeft:
			min = Mathf.Min (0, col - n);
			nthCell = gridLayout [row] [min];
			break;
		case Unit.Direction.LRight:
			min = Mathf.Min (gridLayout [row].Length - 1, col + n);
			nthCell = gridLayout [row] [min];
			break;
		}

		return nthCell;
	}



}
