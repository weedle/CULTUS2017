using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SinglePanelBasicAttack : MonoBehaviour, IntfActionModule
{
    public string actionName;

    public string getActionName()
    {
        return "spba01 - display attk";
    }

    // executes the action
    void IntfActionModule.executeAction(Cell position, Unit.Direction facing)
    {

    }

    // returns a list of units in range (may be empty) of position
    HashSet<Unit> IntfActionModule.findTargetUnits(Cell position, Unit.Direction facing)
    {
        HashSet<Unit> retUnits = new HashSet<Unit>();
        Cell nextCell = GameObject.Find("grid").GetComponent<Grid>().nextCell(position, facing, 1);
        if (nextCell != position)
            if (nextCell.getOccupied())
                retUnits.Add(nextCell.getUnit());

        return retUnits;
    }

    // returns a list of cells in range
    HashSet<Cell> IntfActionModule.findTargetCells(Cell position, Unit.Direction facing)
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
    List<Unit> IntfActionModule.resultOfAction(Cell position,
        Unit.Direction facing, List<Unit> targets)
    {
        return null;
    }

    string IntfActionModule.getActionName()
    {
        return actionName;
    }
}
