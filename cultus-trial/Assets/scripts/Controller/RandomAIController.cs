using UnityEngine;
using System.Collections;

public class RandomAIController : MonoBehaviour, IntfController
{
    Timer thisTimer;
    private bool done = true;

    public bool inProgress()
    {
        return !done;
    }

    public void handleTurn()
    {
        Unit unit = GetComponent<Unit>();
        done = false;
        thisTimer = GameObject.Find("GameLogic").GetComponent<Timer>();
        thisTimer.addTimer(GetComponent<Unit>().id);
    }


    int currTime = 0;
    void Update()
    {
        if (done)
            return;

        if (thisTimer == null)
            return;

        if (thisTimer.checkTimer(GetComponent<Unit>().id, 0.5f) == false)
            return;

        int random = Random.Range(0, 6);

        Unit unit = GetComponent<Unit>();
        switch(random)
        {
            case 0:
                if (unit.currentDir == Unit.Direction.LRight)
                    unit.moveUnit(1);
                else
                    unit.rendToDirection(Unit.Direction.LRight);
                break;
            case 1:
                if (unit.currentDir == Unit.Direction.ULeft)
                    unit.moveUnit(1);
                else
                    unit.rendToDirection(Unit.Direction.ULeft);
                break;
            case 2:
                if (unit.currentDir == Unit.Direction.URight)
                    unit.moveUnit(1);
                else
                    unit.rendToDirection(Unit.Direction.URight);
                break;
            case 3:
                if (unit.currentDir == Unit.Direction.LLeft)
                    unit.moveUnit(1);
                else
                    unit.rendToDirection(Unit.Direction.LLeft);
                break;
            default:
                unit.moveUnit(1);
                break;
        }

        if (unit.movesRemaining == 0)
        {
            done = true;
            unit.done = true;
        }
    }
}
