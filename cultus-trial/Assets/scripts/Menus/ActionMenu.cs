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

    // to call a method, we call highlightOrExecute while
    // specifying which type of component we want to activate
    public void jump()
    {
        highlightOrExecute(typeof(JumpTestAction));
    }

    public void smack()
    {
        highlightOrExecute(typeof(SinglePanelBasicAttack));
    }

    // This function will take a type of action, and highlight
    // the target cells the first time the action is selected.
    // if the action is selected again, it is executed
    // this acts as a sort of confirmation opportunity
    public void highlightOrExecute(System.Type action)
    {
        if (!highlight)
        {
            highlightCells(action);
            highlight = true;
            return;
        }
        execute(action);
    }

    // Grid.highlightThing will highlight the target cells if
    // given an IntfActionModule component
    // here, we call the current unit and pass its action component
    // to Grid.highlightThing
    public void highlightCells(System.Type action)
    {
        Unit unit = GameObject.Find("GameLogic").GetComponent<TurnHandler>().getCurrentUnit();
        GameObject.Find("gridOverlay").GetComponent<Grid>().highlightThing(unit, (IntfActionModule) unit.GetComponent(action.ToString()));
    }

    // Execute gets the corresponding component of the current unit
    // requests said action be executed
    // it also makes the popup menu disappear
    public void execute(System.Type action)
    {
        Unit unit = GameObject.Find("GameLogic").GetComponent<TurnHandler>().getCurrentUnit();
        IntfActionModule module = (IntfActionModule)unit.GetComponent(action.ToString());
        module.executeAction(unit.currentCell, unit.currentDir);
        unit.togglePopUp();
    }
}
