using UnityEngine;
using System;


public class Grid : MonoBehaviour {
	
	Cell[][] gridLayout = new Cell[3][];


	public Grid() {
		Console.Write("So you made a grid...");
	}

	// might contain more variables later
	public struct Cell {
		private bool isOccupied;
		int tileType;
		Vector3 currentPos;
		Unit currentUnit;

		public Cell(int type, Vector3 position, Unit unit){
			tileType = type;
			isOccupied = false;
			currentPos = position;
			currentUnit = unit;
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

		// USAGE: when within the process of emptying a cell of
		//			its unit
		public void unoccupy() {
			if (isOccupied) {
				isOccupied = !isOccupied;
				currentUnit = null;
			}
		}

		// USAGE: when you want to set a new unit to an
		//			EMPTY cell; changes will not proceed if
		//			current cell is occupied
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
		makeGrid ();

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

	public Cell[][] getLayout() {
		return gridLayout;
	}
}
