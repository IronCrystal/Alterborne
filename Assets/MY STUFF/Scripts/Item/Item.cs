using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item : MonoBehaviour{
    public string itemName;
    public int itemID;
    public string itemDesc;
    public Texture2D itemIcon;
    public ItemType itemType;

    public enum ItemType {
        Weapon,
        Apparel,
        Consumable
	};

    public Item(string name, int id, string desc, ItemType type) {
        this.itemName = name;
        this.itemID = id;
        this.itemDesc = desc;
        this.itemType = type;
        this.itemIcon = Resources.Load<Texture2D>("Item Icons/" + name);
    }

	public Item(Item oldItem) : this(oldItem.itemName, oldItem.itemID, oldItem.itemDesc, oldItem.itemType){ }

    public Item() { }
}
