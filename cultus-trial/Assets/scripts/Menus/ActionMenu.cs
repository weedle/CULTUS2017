using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

// First we highlight the given cell, and give a chance for the 
// unit to cancel/confirm the given action
public class ActionMenu : MonoBehaviour, IntfMenu
{
    private List<String> items;
    private List<GameObject> objects;
    private bool highlight = false;

    public void addItem(string itemText)
    {
        GameObject item;
        if (items == null)
        {
            items = new List<string>();
            objects = new List<GameObject>();
            item = Resources.Load("mainbox") as GameObject;
        }
        else
        {
            item = Resources.Load("minibox") as GameObject;
        }
        items.Add(itemText);

        item = Instantiate(item);
        item.name = "actionMenuItem" + items.Count;
        item.GetComponentInChildren<UnityEngine.UI.Text>().text = itemText;

        item.transform.SetParent(gameObject.transform, false);
        item.transform.localScale = new Vector3(1.5f, 0.75f, 1);
        item.transform.position = GameObject.Find("GameLogic").
            GetComponent<TurnHandler>().getCurrentUnit().transform.position
            + new Vector3(0.2f, 2.2f + 0.55f * (items.Count-1), 0);

        switch (itemText)
        {
            case "jump":
                item.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
                item.GetComponent<UnityEngine.UI.Image>().raycastTarget = false;
                item.GetComponent<UnityEngine.UI.Button>()
                    .onClick.AddListener(jump);
                break;
            case "smack":
                item.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
                item.GetComponent<UnityEngine.UI.Image>().raycastTarget = false;
                item.GetComponent<UnityEngine.UI.Button>()
                    .onClick.AddListener(smack);
                break;
        }
        objects.Add(item);
    }

    public void destroyMenu()
    {
        if(items != null)
            items.Clear();
        if(objects != null)
        {
            foreach(GameObject obj in objects)
            {
                Destroy(obj);
            }
        }
        Destroy(gameObject);
    }

    public bool findItem(string itemText)
    {
        throw new NotImplementedException();
    }

    public void jump() 
    {
        Unit unit;
        if (!highlight)
        {
            unit = GameObject.Find("GameLogic").GetComponent<TurnHandler>().getCurrentUnit();
            GameObject.Find("gridOverlay").GetComponent<Grid>().highlightThing(unit, unit.GetComponent<JumpTestAction>());
            highlight = true;
            return;
        }

        print("jump?");
        unit = GameObject.Find("GameLogic").GetComponent<TurnHandler>().getCurrentUnit();
        unit.GetComponent<JumpTestAction>().executeAction(unit.currentCell, unit.currentDir);
        unit.togglePopUp();
    }

    public void smack()
    {
        Unit unit = GameObject.Find("GameLogic").GetComponent<TurnHandler>().getCurrentUnit();
        unit.GetComponent<JumpTestAction>().executeAction(unit.currentCell, unit.currentDir);
    }
}
