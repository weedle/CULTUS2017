using UnityEngine;
using System.Collections;

// Basic interface for menus!
// Depending on the type of menu, some of these functions might be
// unused and/or not do anything. This is okay
public interface IntfMenu {
    // Add an item to the menu, the item denotes the appropriate text
    // if it's an icon-based menu, let the impl class take care of that
    void addItem(string itemText);

    // Find the item and return true if present
    bool findItem(string itemText);

    // Destroying the menu
    void destroyMenu();
}
