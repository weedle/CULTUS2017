using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// For the prototype, let's make this the main unit class
// This will be an abstract class for the full game, of course
public class Unit {
    int currentPos;
    int selectedAttackType;
    string unitType;
    Facing facing;
    public bool active = false;

    public Unit(int pos, int atkType, string type, Facing fce)
    {
        currentPos = pos;
        selectedAttackType = atkType;
        unitType = type;
        facing = fce;
    }

    public enum Facing
    {
        Left,Right,Up,Down
    }

    public List<int> getTargetSquares()
    {
        List<int> retList = new List<int>();

        return retList;
    }

    public void drawUnit()
    {
        if(unitType.Equals("unit1"))
        {
            CommonDefs.drawUnit1(currentPos, facing);
        }
        else if(unitType.Equals("unit2"))
        {
            CommonDefs.drawUnit2(currentPos, facing);
        }
    }

    public void handleUnit()
    {
        if (active)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            if (horizontal != 0)
            {
                if (horizontal < 0)
                    facing = Facing.Left;
                else
                    facing = Facing.Right;
                GameObject.Find("GameLogic").GetComponent<Testing>().redrawUnits();
            }
            if (vertical != 0)
            {
                if (vertical < 0)
                    facing = Facing.Up;
                else
                    facing = Facing.Down;
                GameObject.Find("GameLogic").GetComponent<Testing>().redrawUnits();
            }
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject.Find("GameLogic").GetComponent<Testing>().nextUnit();
            }
        }
    }
}
