using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour{

	// Similar to K-sama's code, this is the main 'UNIT' class
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

    // setUnit sets the internal state of this unit, and then calls displayUnit
    // to reflect that in the game screen
	public void setUnit(Grid.Cell cell, Direction dir, string name, int id) {
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
		

	// USAGE: to actually display this unit
	public void displayUnit() {
        SpriteRenderer spriter;

        // if this game object has a sprite renderer, retrieve it
        // otherwise, generate one
        if (gameObject.GetComponent<SpriteRenderer>() == null)
        {
            spriter = gameObject.AddComponent<SpriteRenderer>();

            // set sorting order so it appears above the tiles
            spriter.sortingOrder = 1;
        }
        else
            spriter = gameObject.GetComponent<SpriteRenderer>();

        transform.position = currentPos;
        name = unitName + id;

        // using SpriteHost to retrieve the image for this unit
        spriter.sprite = GameObject.Find("GameLogic").GetComponent<SpriteHost>()
            .getUnitSprite(unitName, dir);
	}

	public void moveUnit() {
		


	}
}
