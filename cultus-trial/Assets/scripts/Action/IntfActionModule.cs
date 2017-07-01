using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
    This module describes an action available to the unit
    this module is attached to.

    The unit has functions to display all available actions
    and the player can choose which one is needed.

    This module can do the following things:
        a) highlight a target (if applicable)
        b) return target in range, given position and facing
        c) execute the action
*/
public interface IntfActionModule {

    // the name of this action
    string getActionName();

    // executes the action
    void executeAction(Cell position, Unit.Direction facing);

    // returns a list of units in range (may be empty) of position
    HashSet<Unit> findTargetUnits(Cell position, Unit.Direction facing);

    // returns a list of cells in range
    HashSet<Cell> findTargetCells(Cell position, Unit.Direction facing);

    // returns the states of target units after executing the action
    // this assumes every action as a target, is that fair?
    // like, you can attack units, heal units, maybe cast buffs
    // btw this is used to determine the utility of an action
    // we use it for the value iteration routine 
    List<Unit> resultOfAction(Cell position, Unit.Direction facing, List<Unit> targets);
}
