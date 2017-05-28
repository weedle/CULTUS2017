using UnityEngine;
using System.Collections;

public class CommonDefs {
    // Draw a line! Parameters are self-explanatory
    // IT'S SELF-DOCUMENTING CODE :DDD
    static int gridWidth = 5;
    static int gridHeight = 5;
    static float cellSize = 1;

    public static void DrawLine(Vector3 start, Vector3 end, Color color, string lineTag, float duration = 0.2f, float width = 0.075f)
    {
        GameObject myLine = new GameObject("Line");
        myLine.tag = lineTag;
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(width, width);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        if (duration != 0)
            GameObject.Destroy(myLine, duration);
    }

    // Use the DrawLine method to draw a square
    public static void DrawSquare(Vector3 bottomLeft, Vector3 topRight, Color color, string lineTag = "cellLine", float duration = 0.2f, float width = 0.075f)
    {
        Vector3 bottomRight = new Vector3(topRight.x, bottomLeft.y);
        Vector3 topLeft = new Vector3(bottomLeft.x, topRight.y);
        DrawLine(bottomLeft, bottomRight, color, lineTag, duration, width);
        DrawLine(bottomLeft, topLeft, color, lineTag, duration, width);
        DrawLine(topRight, bottomRight, color, lineTag, duration, width);
        DrawLine(topRight, topLeft, color, lineTag, duration, width);
    }

    public static Vector2 getCell(int index)
    {
        // change from 1 to 1.10 to add 10% cell size spacing between cells
        float spacing = cellSize * 1f;
        float x = (index % gridWidth) * spacing;
        float y = -1 * (index / gridWidth) * spacing;
		return new Vector2 (x, y);
    }

    public static void drawCell(int index)
    {
        Vector2 point = CommonDefs.getCell(index);
        Vector2 pointA = new Vector2(cellSize/2, cellSize/2);
        CommonDefs.DrawSquare(point - pointA, point + pointA, Color.green, "cellLine", 0);
    }

    public static void drawCells()
    {
        for (int i = 0; i < gridWidth*gridHeight; i++)
        {
            CommonDefs.drawCell(i);
        }
    }

    public static void drawUnit1(int index, Unit.Facing facing)
    {
        Vector2 cellSize = new Vector2(0.2f, 0.2f);
        CommonDefs.DrawSquare(CommonDefs.getCell(index) - cellSize, CommonDefs.getCell(index) + cellSize, Color.yellow, "unit1", 0, 0.05f);

        Vector2 smallCellSize = new Vector2(0.1f, 0.1f);
        Vector2 offsetFacing = getFacingVector(facing);

        CommonDefs.DrawSquare(CommonDefs.getCell(index) - smallCellSize + offsetFacing, 
            CommonDefs.getCell(index) + smallCellSize + offsetFacing, Color.yellow, "unit1", 0, 0.05f);
    }

    public static void drawUnit2(int index, Unit.Facing facing)
    {
        Vector2 cellSize = new Vector2(0.2f, 0.2f);
        CommonDefs.DrawSquare(CommonDefs.getCell(index) - cellSize, CommonDefs.getCell(index) + cellSize, Color.magenta, "unit2", 0, 0.05f);

        Vector2 smallCellSize = new Vector2(0.1f, 0.1f);
        Vector2 offsetFacing = getFacingVector(facing);

        CommonDefs.DrawSquare(CommonDefs.getCell(index) - smallCellSize + offsetFacing,
            CommonDefs.getCell(index) + smallCellSize + offsetFacing, Color.magenta, "unit2", 0, 0.05f);
    }

    private static Vector2 getFacingVector(Unit.Facing facing)
    {
        Vector2 offsetFacing = Vector2.zero;
        float offset = 0.1f;
        switch (facing)
        {
            case Unit.Facing.Right:
                offsetFacing.x += offset;
                break;
            case Unit.Facing.Left:
                offsetFacing.x -= offset;
                break;
            case Unit.Facing.Down:
                offsetFacing.y += offset;
                break;
            case Unit.Facing.Up:
                offsetFacing.y -= offset;
            break;
        }
        return offsetFacing;
    }

    public void getCellDown(int index)
    {

    }
}
