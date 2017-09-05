using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Structure Terminology
// 	- a 'level' refers to usual sense of a level; specifies the different units, bosses, level map, etc.
// 	- a 'map' refers to the dungeon layout; specifies grid / dungeon layout
//  - a 'grid' refers to a physical "level" / layer of the dungeon layout; specifies a 2D array of cells of a certain height
public class Level : MonoBehaviour {

	private string levelName;
	private List<Unit> allEnemies;
	private List<Unit> allAllies;
	private List<Grid> allGrids;

	// USAGE: literally, indicates the "type" of the level -> FEEL FREE TO MODIFY!
	// NOTE:
	//	- Boss 	   : literally, a level with a boss
	// 	- Tutorial : literally, a tutorial level
	// 	- Town 	   : a non-combat "town" area typical of most RPGs
	// 	- Dungeon  : a combat area typical of most RPGs
	public enum LevelType{
		Boss, Tutorial, Town, Dungeon
	}

	// USAGE: returns how many floors there are in this level
	public int floorCount(){
		return allGrids.Count;
	}

}
