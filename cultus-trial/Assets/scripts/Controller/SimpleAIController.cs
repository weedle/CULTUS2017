using UnityEngine;
using System.Collections;
using System;

public class SimpleAIController : MonoBehaviour, IntfController {

	private bool done = true;

	public bool inProgress(){ 
		return !done;
	}

	//
	public void handleTurn(){


	}

	//
	public void Update(){


	}

	// USAGE: it isn't necessary for an the AI to wait...
	// NOTE: an AI waits for no one!
	public void wait() {
		throw new NotImplementedException ();
	}


}
