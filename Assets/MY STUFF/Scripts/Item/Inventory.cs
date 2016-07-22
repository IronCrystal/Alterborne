using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    public int slotsX, slotsY;
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    public GUISkin skin;
    private bool showInventory;
	public Dictionary<string, Item> database = new Dictionary<string, Item>();

	public Sword swordPrefab;
	public PotionHP potionHPPrefab;

	void Start() {
		for (int i = 0; i < (slotsX * slotsY); i++) {
			slots.Add(new Item());
		}

		//database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
		//inventory.Add(new Sword());
		//inventory.Add(new PotionHP());

		AddItemToDB ("Master Sword", Instantiate(swordPrefab) as Sword);
		AddItemToDB ("HP Potion", Instantiate (potionHPPrefab) as PotionHP);

		AddItemToInv ("Master Sword");
		AddItemToInv ("HP Potion");
	}

	private void AddItemToDB(string name, Item item){
		database [name] = item;
	}

	public Item AddItemToInv(string name){
		Item item = database[name];
		Item newItem = Instantiate(item) as Item;
		newItem.transform.parent = transform.parent;
		inventory.Add (newItem);
		Debug.Log (item);
		//Debug.Log (newItem.itemName);
		//Debug.Log ((Item)newItem);
		return newItem;
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
