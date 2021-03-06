﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

// USAGE: deals damage to ONE of the nearest cells containing a unit that is an enemy of the
// 		  current unit this attack is attached to!
public class SinglePanelBasicAttack : MonoBehaviour, IntfActionModule
{
	private string actionName = "smack";
	public int attackDamage = 5;

	// USAGE: see IntfActionModule
	public int isAttack(){
		return attackDamage;
	}

    // USAGE: returns the name of this attack
    public string getActionName()
    {
        // sample text to return (we'll change this!)
        // later, let's figure out a systematic way of
        // categorizing and naming these
        // return "spba01 - display attk";
		return actionName;
    }

    // USAGE: execute the action 
    public void executeAction(Cell position, Unit.Direction facing)
    {
        HashSet<Unit> units = findTargetUnits(position, facing);
        if(units.Count != 0)
        {
            foreach(Unit unit in units)
            {
                unit.takeDamage(attackDamage);			
            }
			gameObject.GetComponent<Unit> ().canAct = false;
        }
    }

    // returns a list of units in range (may be empty) of position
    public HashSet<Unit> findTargetUnits(Cell position, Unit.Direction facing)
    {
        HashSet<Unit> retUnits = new HashSet<Unit>();

        // for this attack, we only check the one cell
        Cell nextCell = GameObject.Find("grid").GetComponent<Grid>().nextCell(position, facing, 1);
        if (nextCell != position)
            if (nextCell.getOccupied())
                retUnits.Add(nextCell.getUnit());

        // returns empty set if no units in rate
        return retUnits;
    }

    // returns a list of cells in range
    public HashSet<Cell> findTargetCells(Cell position, Unit.Direction facing)
    {
        HashSet<Cell> retCells = new HashSet<Cell>();
        Cell nextCell = GameObject.Find("grid").GetComponent<Grid>().nextCell(position, facing, 1);
        if (nextCell != position)
            retCells.Add(nextCell);
        return retCells;
    }

    // returns the states of target units after executing the action
    // this assumes every action as a target, is that fair?
    // like, you can attack units, heal units, maybe cast buffs
    // btw this is used to determine the utility of an action
    // we use it for the value iteration routine 
    public List<Unit> resultOfAction(Cell position,
        Unit.Direction facing, List<Unit> targets)
    {
        return null;
    }
}
