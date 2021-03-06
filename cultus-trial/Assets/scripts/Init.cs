﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Init : MonoBehaviour {
    public GameObject grid;
    public GameObject gridOverlay;
    private TurnHandler turnHandler;

    void Start () {
		float baseOrthoSize = Screen.height / 64.0f / 1.5f;
		Camera.main.orthographicSize = baseOrthoSize;

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

        turnHandler = GameObject.Find("GameLogic").GetComponent<TurnHandler>();
        turnHandler.init();
        Cell[,] thisGrid = emptyGrid.getLayout ();


		// NOTE: currently the player does not do anything with its list of enemy factions!
		// 			(maybe this will change in the future?
		List<Unit.Faction> playerEF = new List<Unit.Faction> (){ Unit.Faction.Enemy, Unit.Faction.IndepRogue };
        List<string> actions = new List<string>();
        actions.Add("singlepanelbasicattack");
        actions.Add("smacknearby");
        actions.Add("jumptest");
        turnHandler.addUnit(actions, true, thisGrid[0, 0], "flammen", Unit.Faction.Player, Unit.Direction.LRight, 7, playerEF);
        actions.Remove("smacknearby");

        // actions.Clear(); 			// TESTING!!!
		List<Unit.Faction> enemyEF = new List<Unit.Faction>() { Unit.Faction.Player, Unit.Faction.IndepRogue };
        turnHandler.addUnit(actions, false, thisGrid[2, 3], "flammen", Unit.Faction.Enemy, Unit.Direction.LLeft, 8, enemyEF);
        turnHandler.displayUnits();
        
		// zooms into roughly the center cell of grid layout
		Vector3 cameraPos = emptyGrid.getCentrePos ();
		cameraPos.z = -10; 
		Camera.main.transform.position = cameraPos;
    }

    // NOTE: currently only perform movement updates on the 1 fixed unit
    // 		 later we can modify this class to keep track of the
    //			'currently-selected-unit' and only that one can move
    //			at a particular time
    void Update() 
    {

    }
}
