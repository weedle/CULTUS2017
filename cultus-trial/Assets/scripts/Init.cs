using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Init : MonoBehaviour {

	public List<Unit> allUnits;
    public GameObject grid;
    public GameObject gridOverlay;

	void Start () {
		float baseOrthoSize = Screen.height / 64.0f / 1.5f;
		Camera.main.orthographicSize = baseOrthoSize;


        // NOTE: making a new grid (ie. see below) is practically pointless because
        //			it doesn't do anything  --> the current grid-layout is hard-coded
        grid = new GameObject("grid");
        Grid emptyGrid = grid.AddComponent<Grid>();
        emptyGrid.isVirtual = false;
        emptyGrid.makeGrid();
        emptyGrid.loadGrid();

        gridOverlay = new GameObject("gridOverlay");
        Grid overlayGrid = gridOverlay.AddComponent<Grid>();
        overlayGrid.isVirtual = true;
        overlayGrid.makeGrid();
        overlayGrid.loadGrid();

        Cell[,] thisGrid = emptyGrid.getLayout ();
		Cell startCell = thisGrid [0,0];


		// NOTE: all units created must be added to 'allUnits' list
		allUnits = new List<Unit> ();
        GameObject unit1 = new GameObject("char");
        Unit unitComp1 = unit1.AddComponent<Unit>();
        unit1.AddComponent<ManualController>();
        unit1.AddComponent<SinglePanelBasicAttack>().actionName = "testAttack";
        unitComp1.setUnit(startCell, Unit.Direction.LRight, Unit.Faction.Player, "flammen", 007);

        GameObject unit2 = new GameObject("char2");
        Unit unitComp2 = unit2.AddComponent<Unit>();
        unit2.AddComponent<RandomAIController>();
        unitComp2.setUnit(thisGrid[2,2], Unit.Direction.ULeft, Unit.Faction.Player, "flammen", 008);

        allUnits.Add(unitComp1);
        allUnits.Add(unitComp2);
        displayAllUnits ();


		// zooms into roughly the center cell of grid layout
		Vector3 cameraPos = emptyGrid.getCentrePos ();
		cameraPos.z = -10; 
		Camera.main.transform.position = cameraPos;
    }


	public void displayAllUnits() {
		for (int i = 0; i < allUnits.Count; i++) {
			allUnits [i].displayUnit ();
		}
	}


    // NOTE: currently only perform movement updates on the 1 fixed unit
    // 		 later we can modify this class to keep track of the
    //			'currently-selected-unit' and only that one can move
    //			at a particular time
    int unitIndex = 0;
    bool inProgress = false;
    void Update()
    {
        if (inProgress == false)
        {
            allUnits[unitIndex].handleUnit();
            inProgress = true;
        }

        if (allUnits[unitIndex].done == true)
        {
            unitIndex++;
            if (unitIndex >= 2)
                unitIndex = 0;
            inProgress = false;
        }
    }



}
