﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainPop : MonoBehaviour, IntfMenu {

	public void initMenu() {

		// adding appropriate classes to each of the 3 icons
		GameObject child = transform.GetChild (0).gameObject;

        child.AddComponent<ActionIcon> ();
        
		child = transform.GetChild (1).gameObject;
		child.AddComponent<WaitIcon> ();
		child = transform.GetChild (2).gameObject;
		child.AddComponent<ItemIcon> ();


	}

	public void destroyMenu() {


	}

	public bool findItem(string itemText) {
		return true;
	}
		
	public void addItem(string itemText) {

	}

}
