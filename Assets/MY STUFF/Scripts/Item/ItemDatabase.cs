using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour{
	private Dictionary<int, Item> database = new Dictionary<int, Item>(); //keys all items to their ids
    //kept private because we don't want to add tot he database anywhere else but here

    void Awake() //Awake activates before Start. Database is one of the first things that should initialize
    {
        addData(0, new ItemWeapon("Master Sword", 0, "Links's Master Sword", 10, ItemWeapon.AttackType.Forward)); //we could make a text file and read it instead of inputting like this
        addData(1, new ItemConsumable("HP Potion", 1, "Refreshing!", 20, ItemConsumable.HealType.HP));
    }

	void addData(int id, Item item){ 
		database [id] = item;
	}

    public Item getData(int key)
    {
        return database[key]; //returns item at key
    }
}
