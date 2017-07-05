using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainPop : MonoBehaviour, IntfMenu {

	private GameObject relatedUnit;

	public void initMenu(GameObject unit) {
		Debug.Log (unit.name);
		relatedUnit = unit;
		Debug.Log (relatedUnit.name);
    }

	void Update() {


	}

	public void destroyMenu() {
		// un-pausing the unit movement
		ManualController controller = relatedUnit.GetComponent<ManualController>();
		controller.setPause (false);
		Destroy (gameObject);
	}

	public bool findItem(string itemText) {
		return true;
	}
		
	public void addItem(string itemText) {

	}

}
