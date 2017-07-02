using UnityEngine;
using System.Collections;
using System;

public class WaitIcon : MonoBehaviour, IntfMenuItem {
	
    public void activate()
    {
        // this menu item is the child of a menu gameobject
        // transform.parent gets that menu gameobject
        // that menu gameobject is the child of a unit
        // getComponentInParent gets the parent object's Unit component
        Unit unit = transform.parent.GetComponentInParent<Unit>();
        if(unit != null)
            unit.wait();
    }
}
