using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {
	public Dictionary<string, Item> database = new Dictionary<string, Item>();
	public Inventory inv;

    void Start() {
		AddItemToDB ("Sword", new Sword ());
		AddItemToDB ("HP Potion", new PotionHP ());
    }

	void AddItemToDB(string name, Item item){
		database [name] = item;
	}

	public Item AddItemToInv(string name){
		Item item = database[name];
		Item newItem = new Item (item);
		//inv.AddItem (newItem);
		return newItem;
	}
}
