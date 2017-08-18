using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

// USAGE: damages all adjacent/diagonal cells
public class SmackNearby : MonoBehaviour, IntfActionModule
{
    private string actionName = "smacknearby";
    public int attackDamage = 5;

    // USAGE: see IntfActionModule
    public int isAttack()
    {
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
        if (units.Count != 0)
        {
            foreach (Unit unit in units)
            {
                unit.takeDamage(attackDamage);
            }
            gameObject.GetComponent<Unit>().canAct = false;
        }
    }

    // returns a list of units in range (may be empty) of position
    public HashSet<Unit> findTargetUnits(Cell position, Unit.Direction facing)
    {
        HashSet<Unit> retUnits = new HashSet<Unit>();

        HashSet<Cell> targetCells = findTargetCells(position, facing);

        foreach (Cell cell in targetCells)
        {
                if (cell.getOccupied())
                    retUnits.Add(cell.getUnit());
        }

        // returns empty set if no units in rate
        return retUnits;
    }

    // returns a list of cells in range
    public HashSet<Cell> findTargetCells(Cell position, Unit.Direction facing)
    {
        HashSet<Cell> retCells = new HashSet<Cell>();
        Grid grid = GameObject.Find("grid").GetComponent<Grid>();

        Cell lleftCell = grid.nextCell(position, Unit.Direction.LLeft, 1);
        if(lleftCell != position)
        {
            retCells.Add(lleftCell);
            retCells.Add(grid.nextCell(lleftCell, Unit.Direction.ULeft, 1));
            retCells.Add(grid.nextCell(lleftCell, Unit.Direction.LRight, 1));
        }

        Cell uleftCell = grid.nextCell(position, Unit.Direction.ULeft, 1);
        if (uleftCell != position)
        {
            retCells.Add(uleftCell);
            retCells.Add(grid.nextCell(uleftCell, Unit.Direction.LLeft, 1));
            retCells.Add(grid.nextCell(uleftCell, Unit.Direction.URight, 1));
        }

        Cell urightCell = grid.nextCell(position, Unit.Direction.URight, 1);
        if (urightCell != position)
        {
            retCells.Add(urightCell);
            retCells.Add(grid.nextCell(urightCell, Unit.Direction.ULeft, 1));
            retCells.Add(grid.nextCell(urightCell, Unit.Direction.LRight, 1));
        }

        Cell lrightCell = grid.nextCell(position, Unit.Direction.LRight, 1);
        if (lrightCell != position)
        {
            retCells.Add(lrightCell);
            retCells.Add(grid.nextCell(lrightCell, Unit.Direction.LLeft, 1));
            retCells.Add(grid.nextCell(lrightCell, Unit.Direction.URight, 1));
        }
        
        //Cell nextCell = GameObject.Find("grid").GetComponent<Grid>().nextCell(position, facing, 1);
        //if (nextCell != position)
        //    retCells.Add(nextCell);

        //retCells.RemoveWhere(x => x == position);

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
