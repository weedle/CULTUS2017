using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {

	public GameObject mainChar;

	void Start () {
		float baseOrthoSize = Screen.height / 64.0f / 2.0f;
		Camera.main.orthographicSize = baseOrthoSize;


		// NOTE: making a new grid (ie. see below) is practically pointless because
		//			it doesn't do anything  --> the current grid-layout is hard-coded
		Grid emptyGrid = new Grid();	
		emptyGrid.loadGrid ();

	}


}
