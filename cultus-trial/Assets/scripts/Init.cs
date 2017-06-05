using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Init : MonoBehaviour {

	public List<Unit> allUnits;
    public GameObject grid;
    public GameObject gridOverlay;
    private Unit mainUnit1;

	void Start () {
		float baseOrthoSize = Screen.height / 64.0f / 2.0f;
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
        GameObject unit = new GameObject("char");
        mainUnit1 = unit.AddComponent<Unit>();
        mainUnit1.setUnit(startCell, Unit.Direction.LRight, "flammen", 007);
		allUnits.Add (mainUnit1);
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
	void Update() {
		mainUnit1.handleUnit ();
	}



}
