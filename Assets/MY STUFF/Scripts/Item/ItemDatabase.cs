using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {
    public List<Item> items = new List<Item>();

    void Start() {
        items.Add(new Item("Master Sword", 0, "Link's Master Sword", Item.ItemType.Weapon));
        items.Add(new Item("HP Potion", 1, "Refreshing!", Item.ItemType.Consumable));
    }
}
