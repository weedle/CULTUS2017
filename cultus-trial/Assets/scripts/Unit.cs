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
	Direction currentDir;
	Grid.Cell currentCell;

	// NOTE: values current chosen based on what seems to look
	//			right to J-san
	float xCellOffset = -0.03f;
	float yCellOffset = 0.12f;

    // setUnit sets the internal state of this unit, and then calls displayUnit
    // to reflect that in the game screen
	public void setUnit(Grid.Cell cell, Direction dir, string name, int id) {
		updatePos (cell);
		this.currentDir = dir;
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
	// WARNING: this does NOT change the unit display !!!
	// NOTE: might want to revisit this method in a later phase
	public void changeCell(Grid.Cell newCell) {
		if (!newCell.getOccupied ()) {
			currentCell.unoccupy ();
			currentCell = newCell;
			updatePos (newCell);
			newCell.setUnit (this);		// does this work? 
		}
	}
		

	// USAGE: to actually display this unit on screen
	// NOTE: maybe update this function to use the rendToDirection() method?
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
        spriter.sprite = (Sprite) Resources.Load<Sprite>("sprites/" + unitName + directionToString(currentDir));
	}




	// USAGE: handles unit movement corresponding to arrow-key input
	// NOTE: There's probably a better way to write this XD 
	public void handleUnit() {

		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		// left, right arrows
		if (horizontal != 0) {
			
			if (horizontal < 0) {		// left arrow = UL
				if (currentDir == Direction.ULeft)
					moveUnit (1);
				else
					rendToDirection (Direction.ULeft);
				
			} else {		// right arrow = LR
				if (currentDir == Direction.LRight)
					moveUnit (1);
				else
					rendToDirection (Direction.LRight);
			}
		}
		// up, down arrows
		if (vertical != 0) {
			
			if (vertical < 0) {			//down arrow = LL
				if (currentDir == Direction.LLeft)
					moveUnit (1);
				else
					rendToDirection (Direction.LLeft);

			} else {		// up arrow = UR
				if (currentDir == Direction.URight)
					moveUnit (1);
				else
					rendToDirection (Direction.URight);
			}
		}
	}


	// WARNING: assumes current sprite already has a sprite renderer !!!
	public void rendToDirection(Direction newDir) {
		SpriteRenderer spriter = gameObject.GetComponent<SpriteRenderer> ();
		spriter.sprite = (Sprite)Resources.Load<Sprite> ("sprites/" + unitName + directionToString (newDir)); 
	}
		

	// USAGE: moves unit to the n-th cell in the current direction
	public void moveUnit(int n) {
		Grid currentGrid = GameObject.Find ("grid").GetComponent<Grid> ();
		Grid.Cell destCell = currentGrid.nextCell (currentCell, currentDir, n);
		Vector3 newPos = destCell.getPos ();

		changeCell (destCell);
		gameObject.transform.Translate (newPos);
	}



	// WARNING: will return an EMPTY STRING in the case that the
	//				direction variable is screwed up 
    public string directionToString(Direction dir)
    {
        string retStr = "";
        switch(dir)
        {
            case Direction.LLeft:
                retStr = "LL";
                break;
            case Direction.LRight:
                retStr = "LR";
                break;
            case Direction.ULeft:
                retStr = "UL";
                break;
            case Direction.URight:
                retStr = "UR";
                break;
        }
        return retStr;
    }
}
