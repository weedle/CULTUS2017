using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainPop : MonoBehaviour, IntfMenu {

	public void initMenu() {

    }

	void Update() {


	}

	public void destroyMenu() {


		// un-pausing the unit movement
		ManualController controller = transform.parent.GetComponentInParent<ManualController>();
		controller.setPause (false);

	}

	public bool findItem(string itemText) {
		return true;
	}
		
	public void addItem(string itemText) {

	}

}
