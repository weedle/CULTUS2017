using UnityEngine;
using System.Collections;

// basic interface for menu items
public interface IntfMenuItem {

	// USAGE: specifies what happens when item is selected
    void activate();

    void deactivate();
}
