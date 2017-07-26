using UnityEngine;
using System.Collections;
using System;

public class SimpleAIController : MonoBehaviour, IntfController {

	Timer thisTimer;
	private bool done = true;

	// USAGE: see IntfController
	public bool inProgress(){ 
		return !done;
	}


	// USAGE: initialization and timer set-up
	public void handleTurn(){


	}


	// USAGE: attempts to move current unit towards a 'player' unit, attacking if possible
	public void Update(){


	}


	// USAGE: it isn't necessary for an the AI to wait...
	// NOTE: an AI waits for no one!
	public void wait() {
		throw new NotImplementedException ();
	}


}
