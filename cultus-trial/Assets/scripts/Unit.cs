using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	// Similar to K-san's code, this is the main 'UNIT' class
	//	for the prototype --> will convert to abstract when
	// 	we have a clearer notion of a complete game >.<

	string unitName;
	int currentPos; 
	Direction dir;

	public Unit(int pos, Direction dir, string name) {
		currentPos = pos;
		this.dir = dir;
		unitName = name;
	}

	public enum Direction {
		LLeft, LRight, ULeft, URight
	}





}
