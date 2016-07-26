using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour{
	private Dictionary<int, Item> database = new Dictionary<int, Item>();
	//public Inventory inv;

    void Awake()
    {
        addData(0, new ItemWeapon("Master Sword", 0, "Links's Master Sword", 10, ItemWeapon.AttackType.Forward));
        addData(1, new ItemConsumable("HP Potion", 1, "Refreshing!", 20, ItemConsumable.HealType.HP));
    }

	void addData(int id, Item item){
		database [id] = item;
	}

    public Item getData(int key)
    {
        return database[key];
    }
}
