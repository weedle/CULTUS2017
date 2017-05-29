using UnityEngine;
using System;


public class Grid : MonoBehaviour {
	
	Cell[][] gridLayout = new Cell[3][];


	public Grid() {
		Console.Write("So you made a grid...");
	}

	// might contain more variables later
	struct Cell {
		bool occupied;
		int tileType;
		Vector3 currentPos;

		public Cell(int type, Vector3 position){
			tileType = type;
			occupied = false;
			currentPos = position;
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
				Instantiate (thisTile);
			}
		}



	}
}
