using UnityEngine;
using System.Collections;

public class ManualController : MonoBehaviour, IntfController {

    private bool done = true;

    public bool inProgress()
    {
        return !done;
    }

    public void handleTurn()
    {
        Unit unit = GetComponent<Unit>();
        done = false;
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
        if (unit.movesRemaining == 0)
        {
            done = true;
            unit.done = true;
        }
    }
}
