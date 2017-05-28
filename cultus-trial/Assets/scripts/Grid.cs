using UnityEngine;
using System;

public class Grid : MonoBehaviour {
	
	Cell[][] gridLayout = new Cell[2][];

	// might contain more variables later
	struct Cell {
		bool occupied;
		int tileType;

		public Cell(int type){
			tileType = type;
			occupied = false;
		}

		public int getTileType() {
			return tileType;
		}
	}

	public Grid() {
		Console.Write("So you made a grid...");
	}


	// NOTE: the current grid layout is hard-coded for now =.=
	// (/* there's probably a better way to do this... ehue... )
	public void makeGrid() {

		gridLayout [0] = new Cell[4];
		gridLayout [1] = new Cell[3];
		gridLayout [2] = new Cell[2];

		for (int r = 0; r < 2; r++) {
			Cell[] currentRow = gridLayout [r];
			for (int i = 0; i < currentRow.Length - 2; i++) {		// all cells in row 0 and 1 are "2"-tiles, except the laset
				Cell currentCell = new Cell (2);
				currentRow [i] = currentCell;
			}
		}

		gridLayout [0] [4] = new Cell (1);							// cell rows 0 and 1 ends with a "1"-tile
		gridLayout [1] [3] = new Cell (1);

		for (int i = 0; i < gridLayout [2].Length - 1; i++) {		// all cells in row 2 are "1"-tiles
			Cell currentCell = new Cell (1);
			gridLayout [2] [i] = currentCell;
		}
	}



	public void loadGrid() {
		makeGrid ();

		for (int r = 0; r <= 2; r++) {


		}
	}
}
