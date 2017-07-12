using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class ActionIcon : MonoBehaviour, IntfMenuItem {

	public void activate() {
		Unit unit = GameObject.Find ("GameLogic").GetComponent<TurnHandler> ().getCurrentUnit ();
		List<String> allActions = unit.getAvailableActions ();
	}

	public void OnMouseDown() {
		activate();
	}

}
