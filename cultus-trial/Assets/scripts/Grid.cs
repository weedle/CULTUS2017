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

		// NOTE: a NULL UNIT is assigned to Cell upon construction
		public Cell(int type, Vector3 position){
			tileType = type;
			isOccupied = false;
			currentPos = position;
			currentUnit = null;
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
				Cell currentCell = new Cell (1, new Vector3(xVal, yVal));
				currentRow [i] = currentCell;
				xVal += 0.5f;
				yVal += -0.25f;
			}
		}
	}
		

	public void loadGrid() {
		// makeGrid ();

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

}
