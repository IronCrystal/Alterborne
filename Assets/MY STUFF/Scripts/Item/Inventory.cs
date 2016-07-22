using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    public int slotsX, slotsY;
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    public GUISkin skin;

    private ItemDatabase database;
    private bool showInventory;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < (slotsX * slotsY); i++) {
            slots.Add(new Item());
        }
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        inventory.Add(database.items[0]);
        inventory.Add(database.items[1]);
    }

    public void AddItem(Item item) {
        inventory.Add(item);
    }

    public void TurnOnOffInventory() {
        showInventory = !showInventory;
    }

    void OnGUI() {
        GUI.skin = skin;
        if (showInventory) {
            DrawInventory();
        }
    }

    void DrawInventory() {
        int count = 0;
        for (int y = 0; y < slotsY; y++) {
            for (int x = 0; x < slotsX; x++) {
                if (count < inventory.Count) {
                    GUI.Box(new Rect(x * 40, y * 40, 32, 32), inventory[(x % slotsX + y / slotsY)].itemIcon, skin.GetStyle("Slot"));
                    count++;
                }else {
                    GUI.Box(new Rect(x * 40, y * 40, 32, 32), "", skin.GetStyle("Slot"));
                }
            }
        }
        //inventory[(x / slotsX + y % slotsY)].itemName
    }
}
