using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System;

// Structure Terminology
// 	- a 'level' refers to usual sense of a level; specifies the different units, bosses, level map, etc.
// 	- a 'map' refers to the dungeon layout; specifies grid / dungeon layout
//  - a 'grid' refers to a physical "level" / layer of the dungeon layout; specifies a 2D array of cells of a certain height
public class Level : MonoBehaviour {
	public string levelName;
	private string path;
	public Level.LevelType type;

	private List<Unit> allAllies;
	private List<Unit> allEnemies;
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

	// USAGE: return level name
	public string getLevelName(){
		return levelName;
	}
		
	// USAGE: parses 'Level'-related details from XML
	// WARNING: after a 'Level' object has been parsed from a particular file, it CANNOT be
	// 			reparsed from a different file !!!
	public void levelDeserialize(string filepath){

		// minor mod to convert filepath (relative) to an absolute path

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

		// starting that real deserializing action!
		XmlDocument doc = new XmlDocument ();
		doc.Load (filepath);

		// basic Level info
		XmlNode currentNode = doc.SelectSingleNode ("level");
		levelName = currentNode.Attributes ["levelName"].InnerText;
		var temp = currentNode.Attributes ["type"].InnerText;
		type = (Level.LevelType) Enum.Parse (typeof(Level.LevelType), temp);


	}

}
