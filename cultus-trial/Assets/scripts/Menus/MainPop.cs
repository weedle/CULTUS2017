using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class MainPop : MonoBehaviour, IntfMenu {

	void Update() {

    }

    public void destroyMenu()
    {
        Destroy(gameObject);
    }

    public bool findItem(string itemText) {
		return true;
	}
		
	public void addItem(string itemText) {

	}
}
