using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
    public static int moveLimit = 6;
    public int movesRemaining = 6;
    public bool done = true;
	public bool canAct = true;
    public int health = 100;

    // NOTE: values current chosen based on what seems to look
    //			right to J-san
    //float xCellOffset = -0.03f;
    //float yCellOffset = 0.12f;
    // NOTE: K-sama messed with things and updated offset values
    float xCellOffset = 0.03f;
    float yCellOffset = 0.24f;
	float xMenuOffset = -0.70f;
	float yMenuOffset = 0.60f;


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
            GameObject.Find("grid").GetComponent<Grid>()
                .nextCell(currentCell, Direction.LLeft, 0).unoccupy();
            //currentCell.unoccupy ();
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

    
	// USAGE: called when the unit begins its turn; mainly for initialization
	public void handleUnit()
    {
        done = false;
        print("[U:hU] health: " + health);
        movesRemaining = moveLimit;
        displayReachableCells();
        IntfController controller = GetComponent<IntfController>();
        print("in handle unit");
        if (controller != null)
            controller.handleTurn();
    }

    
	// USAGE: renders / "turns" the unit in the specified direction
	public void rendToDirection(Direction newDir) {
		SpriteRenderer spriter = gameObject.GetComponent<SpriteRenderer> ();
        if (spriter == null)
            spriter = gameObject.AddComponent<SpriteRenderer>();

        Sprite[] sprites = Resources.LoadAll<Sprite> ("sprites/" + unitName);
        currentDir = newDir;

        spriter.sprite = sprites[directionToNum(currentDir)];
        displayReachableCells();
    }


    // USAGE: moves unit to the n-th cell in the current direction
	public void moveUnit(int n)
    {
        Grid currentGrid = GameObject.Find ("grid").GetComponent<Grid> ();
		Cell destCell = currentGrid.nextCell (currentCell, currentDir, n);
		Vector3 newPos = destCell.getPos ();
		//Debug.Log (this.currentDir); 			// TESTING!!!

        if (destCell == currentCell)
            return;
		changeCell (destCell);
		// this works because changeCell() also updates the currentPos to the destination
		//	position, for better or for worst
		gameObject.transform.position = currentPos;
        GameObject.Find("gridOverlay").GetComponent<Grid>().hideAll();
        if(currentCell == destCell)
            movesRemaining -= n;
        if (movesRemaining < 0)
            movesRemaining = 0;
        displayReachableCells();
	}

    public void displayReachableCells()
    {
        Grid currentGrid = GameObject.Find("gridOverlay").GetComponent<Grid>();
        HashSet<Cell> cells = currentGrid.getCellsWithinRange(currentCell, movesRemaining);
        GameObject.Find("gridOverlay").GetComponent<Grid>().hideAll();
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


	// USAGE: returns a list of names of all actions available to this unit
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


	// USAGE: handling damage taken and associated consequences
    public void takeDamage(int damage)
    {
        health -= damage;
		Debug.Log ("Just got hit! " + this.unitName + " lost " + damage + " health!!!");
		if (health <= 0) {
			TurnHandler turnH = GameObject.Find ("GameLogic").GetComponent<TurnHandler> ();
			turnH.removeUnit (unitName);
			Destroy (gameObject);
		}
    }
	
	// USAGE: open unit's pop-up menu if there isn't one already
	// NOTE: replace this when we have implemented auto-turn transitions
	// 		 (requires 'BoxCollider2D' component automatically generated for manually controlled units)
	public void OnMouseDown() {
        // only toggle the menu if it's this unit's turn
        if(done == false)
            togglePopUp();
    }

	// USAGE: toggles pop-up menu
	public void togglePopUp() {
        GameObject menu = GameObject.Find("pop-up");
        if (menu != null)
        {
            GetComponent<ManualController>().setPause(false);
            menu.GetComponent<MainPop>().destroyMenu();
            return;
        }
        // sets up menu specifications
        menu = Resources.Load ("icon-group") as GameObject;
		menu.name = "pop-up";
		menu.transform.position = new Vector3 (currentPos.x + xMenuOffset, 
			currentPos.y + yMenuOffset);

        // preventing the unit from moving when the pop-up menu is on-screen
        // Only manually controlled units are clickable, so we can assume this unit
        // has a manual controller
		ManualController controller = GetComponent<ManualController> ();
		controller.setPause (true);

		// creates the menu on screen
		menu = Instantiate (menu);
		menu.name = "pop-up";
	}
}
