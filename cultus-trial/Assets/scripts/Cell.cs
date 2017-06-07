using UnityEngine;
using System.Collections;

public class Cell {
    private bool isOccupied;
    int tileType;
    Vector3 currentPos;
    Unit currentUnit;
    private int row;
    private int col;
    
    // NOTE: a NULL UNIT is assigned to Cell upon construction
    public Cell(int type, Vector3 position, int row, int col)
    {
        tileType = type;
        isOccupied = false;
        currentPos = position;
        currentUnit = null;
        this.row = row;
        this.col = col;
    }

    public int getTileType()
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
}
