using UnityEngine;
using System.Collections;

public class Unit {

	// Similar to K-san's code, this is the main 'UNIT' class
	//	for the prototype --> will convert to abstract when
	// 	we have a clearer notion of a complete game >.<

	string unitName;
	Vector3 currentPos;
	Direction dir;
	Grid.Cell currentCell;


	public Unit(Grid.Cell cell, Direction dir, string name) {
		updatePos (cell);
		this.dir = dir;
		unitName = name;
		cell.setUnit (this);
		currentCell = cell;
	}

	public enum Direction {
		LLeft, LRight, ULeft, URight
	}
		
	public void updatePos(Grid.Cell updatedCell) {
		currentPos = new Vector3 (updatedCell.getPos ().x - 0.03f, 
			updatedCell.getPos ().y + 0.12f);
	}
		
	// WARNING: newCell must be unoccupied for changes to proceed
	// NOTE: maybe this can be changed for the better in the future?
	public void changeCell(Grid.Cell newCell) {


	}

	public void moveUnit() {



	}



}
