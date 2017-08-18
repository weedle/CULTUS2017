using UnityEngine;
using System.Collections.Generic;
using System;

public class ActionIcon : MonoBehaviour, IntfMenuItem
{
    private ActionMenu menu;
    public void activate()
    {
        if (menu != null)
            return;
        Unit unit = GameObject.Find("GameLogic").GetComponent<TurnHandler>().getCurrentUnit();
        List<String> allActions = unit.getAvailableActions();

        menu = new GameObject("actionMenu")
            .AddComponent<ActionMenu>();
        menu.gameObject.transform.parent = GameObject.Find("canvas").transform;
        menu.transform.localScale = new Vector3(0.75f, 0.75f, 1);
        foreach (String action in allActions)
        {
            menu.addItem(action);
        }
    }

    public void deactivate()
    {
        if (menu != null)
            menu.destroyMenu();
    }

    public void OnMouseDown()
    {
        activate();
    }

}