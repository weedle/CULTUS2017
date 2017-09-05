using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

// Structure Terminology
// 	- a 'level' refers to usual sense of a level; specifies the different units, bosses, level map, etc.
// 	- a 'map' refers to the dungeon layout; specifies grid / dungeon layout
//  - a 'grid' refers to a physical "level" / layer of the dungeon layout; specifies a 2D array of cells of a certain height
public class Level : MonoBehaviour {
	public string levelName { get; }
	private string path;
	public Level.LevelType type;

	private List<Unit> allEnemies;
	private List<Unit> allAllies;
	private List<Grid> allGrids;

	// NOTE: currently unused!
	public Level(){
		path = "";
		Debug.Log ("Huhhh, so you made a level!");
	}

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
		
	// USAGE: parsing 'Level'-related details from XML
	// WARNING: after a 'Level' object has been parsed from a particular file, it CANNOT be
	// 			reparsed from a different file !!!
	public void levelParser(string filepath){

		// a few helpful message printouts
		if (path != filepath) {
			Debug.Log ("You can't reparse a different level to this existing level!");
			return;
		} else if (path == filepath) {
			Debug.Log ("... Currently reparsing the level!");
		} else {
			Debug.Log ("... Currently parsing the level!");
			path = filepath;
		}

		// the real parsing action begins!
		XmlDocument doc = new XmlDocument ();
		doc.Load (filepath);

		XmlNode currentNode = doc.SelectSingleNode ("level");
		levelName = currentNode.Attributes ["levelName"].InnerText;
		type = currentNode.Attributes ["type"].InnerText;


	}











}
