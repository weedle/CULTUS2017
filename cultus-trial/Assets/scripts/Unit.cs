using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour{

	// Similar to K-san's code, this is the main 'UNIT' class
	//	for the prototype --> will convert to abstract when
	// 	we have a clearer notion of a complete game >.<

	string unitName;
	int id;
	Vector3 currentPos;
	Direction dir;
	Grid.Cell currentCell;

	// NOTE: values current chosen based on what seems to look
	//			right to J-san
	float xCellOffset = -0.03f;
	float yCellOffset = 0.12f;


	public Unit(Grid.Cell cell, Direction dir, string name, int id) {
		updatePos (cell);
		this.dir = dir;
		unitName = name;
		this.id = id;
		cell.setUnit (this);
		currentCell = cell;
		displayUnit ();
	}

	public enum Direction {
		LLeft, LRight, ULeft, URight
	}


	// WARNING: updatePos does NOT display the updated unit, despite
	//				having changed the position internally
	public void updatePos(Grid.Cell updatedCell) {
		currentPos = new Vector3 (updatedCell.getPos ().x + xCellOffset, 
			updatedCell.getPos ().y + yCellOffset);
	}
		

	// WARNING: newCell must be unoccupied for changes to proceed
	// NOTE: might want to revisit this method in a later phase
	public void changeCell(Grid.Cell newCell) {
		if (!newCell.getOccupied ()) {
			currentCell.unoccupy ();
			currentCell = newCell;
			updatePos (newCell);
			newCell.setUnit (this);		// does this work? 
		}
		displayUnit ();
	}
		

	// USAGE: to actually display this unit (ie. makes a GameObject)
	public void displayUnit() {
		string spriteName = getSprite ();
		GameObject thisUnit = (GameObject)Resources.Load (spriteName);
		thisUnit.transform.position = currentPos;
		thisUnit.name = unitName + id;
		Instantiate (thisUnit);
	}


	public void moveUnit() {
		


	}

	public string getSprite() {
		switch (dir) {
		case Direction.LLeft:
			return unitName + "LL";
		case Direction.LRight:
			return unitName + "LR";
		case Direction.ULeft:
			return unitName + "UL";
		case Direction.URight:
			return unitName + "UR";
		default:
			Debug.Log ("Ya goofed on the unit direction, bruh, but we won't crash ya yet");
			return unitName + "LR";
		}
	}


}
