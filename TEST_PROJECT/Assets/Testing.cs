using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Alright, prototype time.
// This guy will initialize the game board, and create all of the units.
// He will also go through each unit and let the player make their turn.
public class Testing : MonoBehaviour {
    // for the prototype, I'm just gonna keep track of all the units in here
    public List<Unit> units;
    public int activeUnit;
	// Use this for initialization
	void Start ()
    {
        CommonDefs.drawCells();

        units = new List<Unit>();

        units.Add(new Unit(0, 0, "unit1", Unit.Facing.Down));
        units.Add(new Unit(2, 0, "unit1", Unit.Facing.Down));
        units.Add(new Unit(4, 0, "unit1", Unit.Facing.Down));
        units.Add(new Unit(20, 0, "unit2", Unit.Facing.Up));
        units.Add(new Unit(22, 0, "unit2", Unit.Facing.Up));
        units.Add(new Unit(24, 0, "unit2", Unit.Facing.Up));

        redrawUnits();
        activateUnit(0);

        // zoom into center cell
        Vector3 cameraPos = CommonDefs.getCell(12);
        cameraPos.z = -10;

        Camera.main.transform.position = cameraPos;

    }

    public void activateUnit(int index)
    {
        units[index].active = true;
    }

    public void deactivateUnit(int index)
    {
        units[index].active = false;
    }

    public void nextUnit()
    {
        deactivateUnit(activeUnit);
        activeUnit++;
        if (activeUnit == 6)
            activeUnit = 0;
        activateUnit(activeUnit);
    }

    public void redrawUnits()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("unit1"))
        {
            Destroy(obj);
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("unit2"))
        {
            Destroy(obj);
        }
        foreach (Unit unit in units)
        {
            unit.drawUnit();
        }
    }

	// Update is called once per frame
	void Update ()
    {
        units[activeUnit].handleUnit();
        /*
        if(Input.GetButton("Fire1"))
        {
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("cellLine"))
            {
                Destroy(obj);
            }
        }
        */
        print(activeUnit + "    " + Input.GetAxis("Horizontal") + "   " + Input.GetAxis("Vertical"));
    }
}
