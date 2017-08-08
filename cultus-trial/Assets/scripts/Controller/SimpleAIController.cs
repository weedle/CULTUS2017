﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;


// WARNING: this AI will "attack" in the style of a 'EnemyLawful' unit
// 		    (see Faction enum in Unit.cs)
// WARNING: this AIcontroller is optimized for units with 'attacking' actions
// 			in fact, it's pretty useless if the 'attached-to' unit has not attacking actions XD XD XD
public class SimpleAIController : MonoBehaviour, IntfController {

	Timer thisTimer;
	private bool done = true;


	public bool inProgress(){ 
		return !done;
	}


	// USAGE: initialization and timer set-up
	public void handleTurn(){
		Unit unit = GetComponent<Unit> ();
		done = false;
		thisTimer = GameObject.Find ("GameLogic").GetComponent<Timer> ();
		thisTimer.addTimer (unit.id); 
	}



	// USAGE: attempts to move current unit towards a 'player' unit, attacking if possible
	public void Update(){
		Unit u = GetComponent<Unit> ();

		if (done || thisTimer == null)
			return;
		if (thisTimer.checkTimer (u.id, 0.5f) == false)
			return;

		Grid currentGrid = GameObject.Find ("grid").GetComponent<Grid> ();

		Cell[,] allCells = currentGrid.getLayout (); 			 							// TESTING!
		Debug.Log("Last cell is occupied: " + allCells[0,6].getOccupied()); 				// TESTING!

		HashSet<Cell> nearbyCells = currentGrid.getCellsWithinRange(u.currentCell, 1);

		bool madeAttack = attackNearby (nearbyCells, u);
		Debug.Log (madeAttack);


		// checks if a successful attack was performed
		if (madeAttack) { 	 			
			u.movesRemaining -= 1;
		} else {
			
			// at this point, either: 
			// 		- there are no nearby 'player'/'ally' units
			// 		- this unit has no 'attacking' actions, which is unfortunate (see topmost 'WARNING')
			// >> unit will now attempt to move towards a 'player'/'ally' unit

			u.rendToDirection (bestDir (u));
			u.moveUnit (1); 				
		}

		// turn ends if no moves remain
		if (u.movesRemaining == 0) {
			done = true;
			u.done = true;
		}
	}



	// USAGE: returns best direction for unit to take to reach the nearest
	// 		  'player'/'ally' unit
	public Unit.Direction bestDir(Unit u){
		int uRow = u.currentCell.getRow();
		int uCol = u.currentCell.getCol ();
		Cell nearestUC = nearestUnitCell (uRow, uCol);

		if (nearestUC == null) { 				// there are no players/allys on the grid!
			return randomDir ();
		} else { 								// head towards the nearest player/ally
			
			uRow = nearestUC.getRow () - uRow;
			uCol = nearestUC.getCol () - uCol;

			// essentially indicates which axes to move on, based on the largest absolute difference between
			// the player/ally's position and the current unit's position
			int dirIndicator = Math.Max (Math.Abs (uRow), Math.Abs (uCol));

			if (dirIndicator == Math.Abs (uRow)) {  	// largest difference found in the (LLeft, URight) axis
				if (uRow > 0) {
					return Unit.Direction.LLeft;
				} else {
					return Unit.Direction.URight;
				}
			} else { 									// largest difference found in the (ULeft, LRight) axis
				if (uCol > 0) {
					return Unit.Direction.LRight;
				} else {
					return Unit.Direction.ULeft;
				}
			}
		}
	}
		


	// USAGE: returns Cell corresponding to the nearest 'player'/'ally' unit, relative to the 
	// 		  given grid coordinates
	public Cell nearestUnitCell(int unitRow, int unitCol){
		// finds all 'player' and 'ally' units, if any
		GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
		GameObject[] allAllys = GameObject.FindGameObjectsWithTag ("Allied");

		if (allPlayers.Length == 0 && allAllys.Length == 0)
			return null;

		// attempts to find nearest 'player'/'ally'unit
		Cell nearestU = null;

		// TODO: this is just a large random number; please change this appropriately!
		int evalH = 1000;

		GameObject[] goodGuys = allPlayers.Concat (allAllys).ToArray ();
		foreach (GameObject g in goodGuys) {
			Cell thisC = g.GetComponent<Unit> ().currentCell;

			// thisH should be the # of moves it for AI-controlled unit to reach this 'player'/'ally' unit ... I think >.<
			int thisH = Mathf.Abs((thisC.getRow () - unitRow + (thisC.getCol () - unitCol)));

			if (thisH < evalH) {
				evalH = thisH;
				nearestU = thisC;
			}
		}
		return nearestU;
	}



	// USAGE: returns a crude randomly-generated direction
	// NOTE: use this in the case there are no player/ally units to head towards
	public Unit.Direction randomDir(){
		Array dVals = Enum.GetValues (typeof(Unit.Direction));
		Unit.Direction rDir = (Unit.Direction)dVals.GetValue (UnityEngine.Random.Range (0, dVals.Length));
		return rDir;
	}



	// USAGE: if there is a nearby 'player'/'ally' unit, attack it
	// 		  return true if there was a successful attack
	public bool attackNearby(HashSet<Cell> nearbyCells, Unit u){
		bool madeAttack = false;

		Debug.Log ("There are " + nearbyCells.Count + " nearby cells!");
		foreach (Cell c in nearbyCells){
			if (!c.getOccupied ()) {
				Debug.Log ("Cell at [" + c.getRow() + "," + c.getCol() + "] is not occupied...");
				continue;
			}

			Debug.Log ("Got to line 165 in SimpleAIController");
			Unit.Faction f = c.getUnit ().unitFaction;
			if (f == Unit.Faction.Player || f == Unit.Faction.Allied) {  // found a player / ally --> let's attack it!
				Debug.Log("Found a player/allied unit!");
				IntfActionModule attack = findBestAttack (u);
				attack.executeAction (u.currentCell, u.currentDir);
				madeAttack = true;
				break;
			}
		}
		return madeAttack;
	}



	// USAGE: finds the attack action with the highest damage value, if such exists
	public IntfActionModule findBestAttack(Unit u){
		IntfActionModule[] allActions = u.GetComponents<IntfActionModule> ();
		IntfActionModule bestAttack = null;
		int attackVal = 0;

		foreach (IntfActionModule a in allActions) {
			if (a.isAttack() > attackVal)
				bestAttack = a;
		}
		return bestAttack;
	}



	// USAGE: it isn't necessary for an the AI to wait...
	// NOTE: an AI waits for no one!!! *DRAMATIC	MUSIC*
	public void wait() {
		throw new NotImplementedException ();
	}
}
