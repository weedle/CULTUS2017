using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnHandler : MonoBehaviour
{
    // this guy keeps track of all units in play
    private List<Unit> allUnits;
    int unitIndex = 0;
    bool death = false;
    bool inProgress = false;


	public void init()
	{
		allUnits = new List<Unit>();
	}


	// Update is called once per frame
	void Update ()
    {
        // if we have no units in play, this class does nothing
        if (allUnits.Count == 0)
            return;
        
        // if the unit is not currently executing its turn, 
        // please request it to do so
        if (inProgress == false)
        {
            allUnits[unitIndex].handleUnit();
            inProgress = true;
        }

        // If the unit we're in control of dies, we do not increase unitIndex
        // if we're on the last unit, index goes to 0
        if (death)
        {
            if (unitIndex >= allUnits.Count)
                unitIndex = 0;
            if (allUnits[unitIndex].done == true)
            {
                unitIndex++;
                if (unitIndex >= allUnits.Count)
                    unitIndex = 0;
                inProgress = false;
            }
        }

        // if the unit is done, please request the next
        // unit to move
        if (allUnits[unitIndex].done == true)
        {
            unitIndex++;
            if (unitIndex >= allUnits.Count)
                unitIndex = 0;
            inProgress = false;
        }
        death = false;
    }



    // add a new unit to the field
    public void addUnit(List<string> actions, bool isSelectable, Cell startCell, string unitName, Unit.Faction faction, 
		Unit.Direction facing, int id, List<Unit.Faction> enemyFactions)
    {
        if(allUnits == null)
            allUnits = new List<Unit>();

        // the unit will set its proper name later
        GameObject newUnit = new GameObject("newUnit");

		// adds appropriate tag in reference to the unit's faction
		newUnit.tag = faction.ToString();

        // BoxCollider is needed to make this unit clickable
        if (isSelectable)
        {
            newUnit.AddComponent<BoxCollider2D>();
        }

        // For now, all units get a basic ai controller except
        // units directly controlled by the player
        if(faction == Unit.Faction.Player)
            newUnit.AddComponent<ManualController>();
        else
            // newUnit.AddComponent<RandomAIController>(); 		
			newUnit.AddComponent<SimpleAIController>(); 			/// MODIFED THIS HERE FOR TESTING !!! ///

        // add the appropriate IntfActionModule impls to the unit
        foreach(string action in actions)
        {
            switch(action)
            {
                case "singlepanelbasicattack":
                    newUnit.AddComponent<SinglePanelBasicAttack>();
                    break;
                case "jumptest":
                    newUnit.AddComponent<JumpTestAction>();
                    break;
            }
        }

        Unit unitComp = newUnit.AddComponent<Unit>();
        unitComp.setUnit(startCell, facing, Unit.Faction.Player, unitName, id, enemyFactions);
        startCell.setUnit(unitComp);
        allUnits.Add(unitComp);
    }

    //display units all on field
    public void displayUnits()
    {
        for (int i = 0; i < allUnits.Count; i++)
        {
            allUnits[i].displayUnit();
        }
    }

    public Unit getCurrentUnit()
    {
        return allUnits[unitIndex];
    }

	// USAGE: removes unit with specified name from allUnits, if such exists
	// NOTE: in case of duplicates, it will only eliminate the "first" one
	public void removeUnit(string unitName)
	{
		foreach (Unit u in allUnits){
			if (u.unitName == unitName) {
				allUnits.Remove (u);
                death = true;
				return;
			}
		}
	}

    //on unit death, call this function so we can check the game status
    // method impl: go through all units on field, and check their faction
    // if all enemies are dead, call victory
    // if we want to have other win conditions, this is where we'd start
    // note that if there are other win conditions, we need to add extra 
    // calls to checkGameStatus
    public void checkGameStatus()
    {
        bool allGood = true;
        bool allEvil = true;
        foreach (Unit unit in allUnits)
        {
            if (unit.unitFaction == Unit.Faction.Enemy)
            {
                allGood = false;
            }
            if (unit.unitFaction == Unit.Faction.Player ||
                unit.unitFaction == Unit.Faction.Allied)
            {
                allEvil = false;
            }
        }

        if(allGood)
        {
            Debug.Log("alll units are good, call victory scene");
        }

        if (allEvil)
        {
            Debug.Log("alll units are bad, call lose scene");
        }

        this.enabled = false;
    }
}
