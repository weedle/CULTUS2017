using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour{

    // Similar to K-sama's code, this is the main 'UNIT' class
    //	for the prototype --> will convert to abstract when
    // 	we have a clearer notion of a complete game >.<

    public string unitName;
    public int id;
    public Faction unitFaction;
	public Vector3 currentPos;
    public Direction currentDir;
    public Cell currentCell;
	//public PopUpHandler menuHandler;			// handles pop-up selection menu
    public static int moveLimit = 6;
    public int movesRemaining = 6;
    public bool done = true;
    public int health = 100;
    // NOTE: values current chosen based on what seems to look
    //			right to J-san
    //float xCellOffset = -0.03f;
    //float yCellOffset = 0.12f;
    // NOTE: K-sama messed with things and updated offset values
    float xCellOffset = 0.03f;
    float yCellOffset = 0.24f;

	

    // setUnit sets the internal state of this unit, and then calls displayUnit
    // to reflect that in the game screen
    public void setUnit(Cell cell, Direction dir, Faction faction, string name, int id) {
		updatePos (cell);
		this.currentDir = dir;
		unitName = name;
        unitFaction = faction;
		this.id = id;
		cell.setUnit (this);
		currentCell = cell;
		displayUnit ();
		gameObject.AddComponent<BoxCollider2D> ();		// added 2D collider to OnMouseDown() access
	}

	public enum Direction {
		LLeft, LRight, ULeft, URight
	}

    // Factions
    // Player: under player control
    //      ie; a player unit
    // Allied: under AI control, but allied with Player
    //      seeks out and attacks Enemy units, IndepEnemy if nearby
    // Passive: never attacks, flees if attacked
    // IndepAlly: attacks nearby enemies, otherwise wanders
    // IndepEnemy: attacks nearby players, otherwise wanders
    // IndepNeutral: wanders, changes to IndepAlly/IndepEnemy if attacked
    //      eg; a wolf that can be angered by player or enemy units
    //      why not change to Allied or Enemy? Because if the goal of a mission
    //      is to, for example, defeat all enemies, angered mobs shouldn't count
    //  IndepRogue: attacks all nearby nonIndepRogue units
    //      eg; hungry pack of wolves
    // EnemyLawful: seeks out and attacks Player and Allied units
    // EnemyChaotic: seeks out and attacks all nonEnemy units
    
    // I know it seems complicated, but if you abstract out the targets, it's
    // really the same
    public enum Faction {
        Player, Allied, Enemy, IndepPassive, IndepRogue, IndepNeutral
    }
	// WARNING: updatePos does NOT display the updated unit, despite
	//				having changed the position internally
	public void updatePos(Cell updatedCell) {
		currentPos = new Vector3 (updatedCell.getPos ().x + xCellOffset, 
			updatedCell.getPos ().y + yCellOffset);
	}
		

	// WARNING: newCell must be unoccupied for changes to proceed
	// WARNING: this does NOT change the unit display !!!
	public void changeCell(Cell newCell) {
		if (!newCell.getOccupied ()) {
			currentCell.unoccupy ();
			currentCell = newCell;
			updatePos (newCell);
			newCell.setUnit (this);		
		}
	}
		

	// USAGE: to actually display this unit on screen
	public void displayUnit() {
        SpriteRenderer spriter;

        // if this game object has a sprite renderer, retrieve it
        // otherwise, generate one
        if (gameObject.GetComponent<SpriteRenderer>() == null)
        {
            spriter = gameObject.AddComponent<SpriteRenderer>();

            // set sorting order so it appears above the tiles
            spriter.sortingLayerName = "Units";
        }
        else
            spriter = gameObject.GetComponent<SpriteRenderer>();

        transform.position = currentPos;
        name = unitName + id;
        
        Sprite[] sprites = Resources.LoadAll<Sprite>("sprites/" + unitName);
        spriter.sprite = sprites[directionToNum(currentDir)];
    }

    
	public void handleUnit()
    {
        done = false;
        print("[U:hU] health: " + health);
        movesRemaining = moveLimit;
        displayReachableCells();
        IntfController controller = GetComponent<IntfController>();
        if (controller != null)
            controller.handleTurn();
    }

    
	public void rendToDirection(Direction newDir) {
		SpriteRenderer spriter = gameObject.GetComponent<SpriteRenderer> ();
        if (spriter == null)
            spriter = gameObject.AddComponent<SpriteRenderer>();

        Sprite[] sprites = Resources.LoadAll<Sprite> ("sprites/" + unitName);
        currentDir = newDir;

        displayReachableCells();
        spriter.sprite = sprites[directionToNum(currentDir)];
	}


    // USAGE: moves unit to the n-th cell in the current direction
	public void moveUnit(int n) {
		Grid currentGrid = GameObject.Find ("grid").GetComponent<Grid> ();
		Cell destCell = currentGrid.nextCell (currentCell, currentDir, n);
		Vector3 newPos = destCell.getPos ();

        if (destCell == currentCell)
            return;
		changeCell (destCell);
		// this works because changeCell() also updates the currentPos to the destination
		//	position, for better or for worst
		gameObject.transform.position = currentPos;
        GameObject.Find("gridOverlay").GetComponent<Grid>().hideAll();
        if(currentCell == destCell)
            movesRemaining -= n;
        displayReachableCells();
	}

    private void displayReachableCells()
    {
        Grid currentGrid = GameObject.Find("gridOverlay").GetComponent<Grid>();
        HashSet<Cell> cells = currentGrid.getCellsWithinRange(currentCell, movesRemaining);
        GameObject.Find("gridOverlay").GetComponent<Grid>()
            .highlightCells(cells, (Sprite)Resources.Load<Sprite>("sprites/movementMarker"));

    }


    public int directionToNum(Direction dir)
    {
        switch(dir)
        {
            case Direction.LLeft:
                return 2;
            case Direction.LRight:
                return 3;
            case Direction.ULeft:
                return 0;
            case Direction.URight:
                return 1;
            default:
                return 0;
        }
    }


	// USAGE: returns a list of names of the actions available to this unit
    public List<string> getAvailableActions()
    {
        IntfActionModule[]actions = GetComponents<IntfActionModule>();
        List<string> actionStrings = new List<string>();
        foreach(IntfActionModule action in actions)
        {
            actionStrings.Add(action.getActionName());
        }
        return actionStrings;
    }


	// USAGE: clicking on the unit should display the unit's pop-up menu
	// NOTE: added a 2DCollider to this unit to make this work
	void OnMouseDown() {
        /*
		if (GetComponent<PopUpHandler> () == null)
			menuHandler = gameObject.AddComponent<PopUpHandler> ();
		menuHandler.displayPopUp ();
        */
	}

    public void takeDamage(int damage)
    {
        health -= damage;
        // handle death somehow?
    }

}
