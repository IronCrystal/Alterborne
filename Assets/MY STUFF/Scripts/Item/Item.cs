using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {
    public string itemName;
    public int itemID;
    public string itemDesc;
    public Sprite itemIcon;
    public ItemType itemType;

    public enum ItemType {
        Weapon,
        Apparel,
        Consumable,
        Misc,
        Quest
	};

    public Item(string name, int id, string desc, ItemType type) {
        this.itemName = name;
        this.itemID = id;
        this.itemDesc = desc;
        this.itemType = type;
        this.itemIcon = Resources.Load<Sprite>("Item Icons/" + name);
    }

    public Item() { }
}
