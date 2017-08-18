using UnityEngine;
using System.Collections;

public interface IntfController {
	
	// USAGE: returns true, if we are currently the corresponding unit's turn
	//        false, otherwise
    bool inProgress();

	// USAGE: turn initialization and general set-up
    void handleTurn();

    void wait();
}
