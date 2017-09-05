using UnityEngine;
using System.Collections;

public class Cell {
    private bool isOccupied;
	Cell.TileType tileType;
    Vector3 currentPos;
    Unit currentUnit;
    private int row;
    private int col;

    public static float xOffset = 1f;
    public static float yOffset = 0.5f;
    
    // NOTE: a NULL UNIT is assigned to Cell upon construction
	public Cell(Cell.TileType type, Vector3 position, int row, int col)
    {
        tileType = type;
        isOccupied = false;
        currentPos = position;
        currentUnit = null;
        this.row = row;
        this.col = col;
    }

	// USAGE: introducing some new tile types to make the grid more interesting! FEEL FREE TO MODIFY !!!
	// NOTE:
	// 	- Stairs : entry + exit tiles used to move between adjacent floors
	// 	- Portal : entry + exit tiles used to move between other Portal tiles (within / between floors)
	// 	- Hazard : dangerous tiles that drains health!
	//	- Spawner : tiles where enemies can spawn from!
	// 	- Default : nothing too special about this tile XD
	public enum TileType{
		Stairs, Portal, Hazard, Spawner, Default
	}
		

	public Cell.TileType getTileType()
    {
        return tileType;
    }
    public Vector3 getPos()
    {
        return currentPos;
    }
    public void setPos(Vector3 position)
    {
        currentPos = position;
    }
    public bool getOccupied()
    {
        return isOccupied;
    }
    public int getRow()
    {
        return row;
    }
    public int getCol()
    {
        return col;
    }

    // USAGE: use within the process of emptying a cell of
    //			its unit
    // WARNING: unoccupy() will only proceed if this cell
    //				is actually occupied
    // NOTE: might want to revisit this method at a later phase
    public void unoccupy()
    {
        if (isOccupied)
        {
            Debug.Log("unoccupying cell at " + row + ":" + col + " " + Time.time.ToString());
            isOccupied = !isOccupied;
            currentUnit = null;
        }
    }

    // USAGE: when you want to set a new unit to an
    //			EMPTY cell; changes will not proceed if
    //			current cell is occupied
    // NOTE: might want to revisit this method at a later phase
    public void setUnit(Unit unit)
    {
        if (!isOccupied)
        {
            currentUnit = unit;
            isOccupied = true;
        }
    }

    public Unit getUnit()
    {
        return currentUnit;
    }
}
