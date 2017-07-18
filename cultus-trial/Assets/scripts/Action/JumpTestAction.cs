using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


/* ACTION: 
 * 	- moves unit a set amount of spaces in the direction it is currently
 * 		facing, if possible
 * 	- if this isn't possible, then do nothing ("turn" does not decrease though)
 */
public class JumpTestAction : MonoBehaviour, IntfActionModule {

	private string actionName = "jump";
	private int numJumpSpaces = 3;

	public string getActionName() {
		return actionName;
	}


	// USAGE: attempts to "jump" the current unit into desired spot,
	// 			if said spot is open; do nothing otherwise
	// WARNING: allTargets.Single will work ONLY if there is exactly
	// 			one item in the hashset !!! (ok for this case)
	public void executeAction(Cell position, Unit.Direction facing) {
		Unit thisUnit = gameObject.GetComponent<Unit> ();
		HashSet<Cell> allTargets = findTargetCells(position, facing);

		if (allTargets.Count != 0) {
			Cell newCell = allTargets.Single ();
			if (!newCell.getOccupied ()) {
				thisUnit.moveUnit (numJumpSpaces);
				thisUnit.canAct = false;
			}
		}
	}


	// returns a list of units in range (may be empty) of position
	// WARNING: null is returned since this action does not affect ANY units
	public HashSet<Unit> findTargetUnits(Cell position, Unit.Direction facing) {
		return new HashSet<Unit>();
	}

	// returns a list of cells in range
	// NOTE: the returned list is either EMPTY or contains ONE cell
	public HashSet<Cell> findTargetCells(Cell position, Unit.Direction facing) {
		HashSet<Cell> targetCells = new HashSet<Cell> ();
		Cell jumpToCell = GameObject.Find ("grid").GetComponent<Grid> ().
			nextCell (position, facing, numJumpSpaces);

		if (jumpToCell != null)
			targetCells.Add (jumpToCell);

		return targetCells;
	}


	// returns the states of target units after executing the action
	// this assumes every action as a target, is that fair?
	// like, you can attack units, heal units, maybe cast buffs
	// btw this is used to determine the utility of an action
	// we use it for the value iteration routine 
	// WARNING: currently only returns null --> not needed for this action
	public List<Unit> resultOfAction(Cell position, 
		Unit.Direction facing, List<Unit> targets) {
		return null;
	}


	// USAGE: change the # of spaces this unit can "jump"; default == 3
	public void setNumJumpSpaces(int n) {
		numJumpSpaces = n;
	}
}
