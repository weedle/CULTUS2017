using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ManualController : MonoBehaviour, IntfController {
    IntfActionModule[] actions;
    int attackIndex = 0;

    private bool done = true;

    public bool inProgress()
    {
        return !done;
    }

    public void handleTurn()
    {
        Unit unit = GetComponent<Unit>();
        done = false;
        actions = GetComponents<IntfActionModule>();
    }

    void Update()
    {
        if (done)
            return;

        Unit unit = GetComponent<Unit>();
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {   // right arrow = LR
            if (unit.currentDir == Unit.Direction.LRight)
                unit.moveUnit(1);
            else
                unit.rendToDirection(Unit.Direction.LRight);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {   // left arrow = UL
            if (unit.currentDir == Unit.Direction.ULeft)
                unit.moveUnit(1);
            else
                unit.rendToDirection(Unit.Direction.ULeft);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {   // up arrow = UR
            if (unit.currentDir == Unit.Direction.URight)
                unit.moveUnit(1);
            else
                unit.rendToDirection(Unit.Direction.URight);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {   //down arrow = LL
            if (unit.currentDir == Unit.Direction.LLeft)
                unit.moveUnit(1);
            else
                unit.rendToDirection(Unit.Direction.LLeft);
        }
        // note: this will be changed so we use Jenne's nifty menus
        else if(Input.GetButtonDown("Fire1") &&
            actions.Length != 0)
        {
            print("gonna attack: " + actions[attackIndex].getActionName());

            HashSet<Cell> targetCells = GetComponent<IntfActionModule>().findTargetCells(unit.currentCell, unit.currentDir);

            // note: for single panel, cell is either yellow or red
            // but for multiple panels, we'll have to iterate through them
            if (GetComponent<IntfActionModule>().findTargetUnits(unit.currentCell, unit.currentDir).Count != 0)
            {
                // yellow highlight to show target cells
                GameObject.Find("gridOverlay").GetComponent<Grid>()
                    .highlightCells(targetCells, (Sprite)Resources.Load<Sprite>("sprites/attackMarker"));
                //GetComponent<IntfActionModule>().executeAction(unit.currentCell, unit.currentDir);
                //unit.movesRemaining = 0;
            }
            else
            {
                // red highlight to show target cells
                GameObject.Find("gridOverlay").GetComponent<Grid>()
                    .highlightCells(targetCells, (Sprite)Resources.Load<Sprite>("sprites/highlightMarker"));
            }
            attackIndex++;
            if(attackIndex >= actions.Length)
            {
                attackIndex = 0;
            }
        }
        if (unit.movesRemaining == 0)
        {
            done = true;
            unit.done = true;
        }
    }

    bool IntfController.inProgress()
    {
        throw new NotImplementedException();
    }

    void IntfController.handleTurn()
    {
        throw new NotImplementedException();
    }

    void IntfController.wait()
    {
        Unit unit = GetComponent<Unit>();
        unit.movesRemaining = 0;
    }
}
