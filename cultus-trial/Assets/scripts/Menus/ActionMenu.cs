using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

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
                print("added jump listener");
                item.GetComponent<UnityEngine.UI.Button>()
                    .onClick.AddListener((delegate { jump(); }));
                break;
            case "smack":
                print("added smack listener");
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
        print("jump?");
        Unit unit = GameObject.Find("GameLogic").GetComponent<TurnHandler>().getCurrentUnit();
        unit.GetComponent<JumpTestAction>().executeAction(unit.currentCell, unit.currentDir);
    }

    public void smack()
    {
        Unit unit = GameObject.Find("GameLogic").GetComponent<TurnHandler>().getCurrentUnit();
        print("smack?");
    }
}
