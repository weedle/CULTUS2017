using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;


// WARNING: this AI will "attack" in the style of a 'EnemyLawful' unit
// 		    (see Faction enum in Unit.cs)
// WARNING: this AIcontroller is optimized for units with 'attacking' actions
public class SimpleAIController : MonoBehaviour, IntfController {

	Timer thisTimer;
	private bool done = true;

	// USAGE: see IntfController
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
		HashSet<Cell> nearbyCells = currentGrid.getCellsWithinRange(u.currentCell, 1);

		bool madeAttack = attackNearby (nearbyCells, u);

		// checks if a successful attack was performed
		if (madeAttack) { 	 			
			u.movesRemaining -= 1;
		} else {
			
			// at this point, either: 
			// 		- there are no nearby 'player'/'ally' units
			// 		- this unit has no 'attacking' actions
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
	// NOTE: there is probably a better way to do this =.=|||
	public Unit.Direction bestDir(Unit u){
		Cell nearestUC = nearestUnitCell (u);

		/*
		if (nearestUC == null) { 				// there are no players/allys on the grid!
			return randomDir ();
		} else { 								// head towards the nearest player/ally
			


		}
			
		return Unit.Direction.LLeft;
		*/
		return randomDir ();
	}



	// USAGE: returns the nearest 'player'/'ally' to the given unit; otherwise, null
	public Cell nearestUnitCell(Unit u){
		// finds all 'player' and 'ally' units, if any
		GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
		GameObject[] allAllys = GameObject.FindGameObjectsWithTag ("Allied");

		if (allPlayers.Length == 0 && allAllys.Length == 0)
			return null;

		// attempts to find nearest 'player'/'ally'unit
		int uRow = u.currentCell.getRow();
		int uCol = u.currentCell.getCol ();
		Cell nearestU = null;
		int evalH = -1;

		GameObject[] goodGuys = allPlayers.Concat (allAllys).ToArray ();
		foreach (GameObject g in goodGuys) {
			Cell thisC = g.GetComponent<Unit> ().currentCell;

			// thisH should be the # of moves it for AI-controlled unit to reach this 'player'/'ally' unit ... I think >.<
			int thisH = Mathf.Abs((thisC.getRow () - uRow) + (thisC.getCol () - uCol));

			if (thisH > evalH) {
				evalH = thisH;
				nearestU = thisC;
			}
		}
		return nearestU;
	}
		

	// USAGE: returns a crude randomly-generated direction
	// NOTE: use this in the case there are no player/ally units to head towards
	// WARNING: not sure if this works :) please test!
	public Unit.Direction randomDir(){
		


	}


	// USAGE: if there is a nearby 'player'/'ally' unit, attack it
	// 		  return true if there was a successful attack
	public bool attackNearby(HashSet<Cell> nearbyCells, Unit u){
		bool madeAttack = false;

		foreach (Cell c in nearbyCells){
			if (!c.getOccupied ()) {
				continue;
			}

			Unit.Faction f = c.getUnit ().unitFaction;
			if (f == Unit.Faction.Player || f == Unit.Faction.Allied) { 		// found a player / ally --> let's attack it!
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
