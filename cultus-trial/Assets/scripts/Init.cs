using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Init : MonoBehaviour {

	public GameObject mainChar;
	public List<Unit> allUnits;
    public GameObject grid;

	void Start () {
		float baseOrthoSize = Screen.height / 64.0f / 2.0f;
		Camera.main.orthographicSize = baseOrthoSize;


        // NOTE: making a new grid (ie. see below) is practically pointless because
        //			it doesn't do anything  --> the current grid-layout is hard-coded
        grid = new GameObject("grid");
        Grid emptyGrid = grid.AddComponent<Grid>();	
		emptyGrid.makeGrid ();
		emptyGrid.loadGrid ();

		Grid.Cell[][] thisGrid = emptyGrid.getLayout ();
		Grid.Cell startCell = thisGrid [0] [0];


		// NOTE: all units created must be added to 'allUnits' list
		allUnits = new List<Unit> ();
        GameObject unit = new GameObject("char");
        Unit mainUnit1 = unit.AddComponent<Unit>();
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



}
