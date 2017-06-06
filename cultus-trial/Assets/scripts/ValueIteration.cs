using UnityEngine;
using System.Collections;

// Attach this to a unit, and that unit can call this to figure
// out the best cell to move to and the best action to take is
public class ValueIteration {

	struct State
    {
        Cell position;
        Unit.Direction facing;
        string action;
    }
}
