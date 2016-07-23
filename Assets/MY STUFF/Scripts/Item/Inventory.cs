using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public int slotsX, slotsY;
    public List<Sprite> inventory = new List<Sprite>();
    //public List<Texture2D> slots = new List<Texture2D>();
    public GUISkin skin;
    private bool showInventory;
	public Dictionary<string, Item> database = new Dictionary<string, Item>();

	public Sword swordPrefab;
	public PotionHP potionHPPrefab;
    public Button slot;

	void Start() {
		AddItemToDB ("Master Sword", Instantiate(swordPrefab));
		AddItemToDB ("HP Potion", Instantiate (potionHPPrefab));
        
		AddItemToInv ("Master Sword");
		AddItemToInv ("HP Potion");

        InitInventoryGUI();
	}

	private void AddItemToDB(string name, Item item){
		database [name] = item;
	}

	public void AddItemToInv(string name){
		Item item = database[name];
		inventory.Add (item.itemIcon);
	}
		
    public void TurnOnOffInventory() {
        showInventory = !showInventory;
        ShowHideInventory(showInventory);
    }

    void InitInventoryGUI()
    {
        int count = 0;
        Button slotXY;
        int invX = 0;
        int invY = 0;
        for (int y = slotsY - 1; y >= 0; y--)
        {
            for (int x = 0; x < slotsX; x++)
            {
                if (count < inventory.Count)
                {
                    
                    slotXY = Instantiate(slot, new Vector3(x * 40 + 40, y * 40 - 50 * slotsY, 0), transform.rotation) as Button;
                    slotXY.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = inventory[invX + invY*slotsY];
                    slotXY.gameObject.SetActive(false);
                    
                    slotXY.transform.SetParent(transform, false);
                    count++;
                }
                else
                {
                    slotXY = Instantiate(slot, new Vector3(x * 40 + 40, y * 40 - 50 * slotsY, 0), transform.rotation) as Button;
                    slotXY.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = slotXY.gameObject.GetComponent<Image>().sprite;
                    slotXY.gameObject.SetActive(false);
                    slotXY.transform.SetParent(transform, false);
                }
                invX++;
            }           
            invY++;
        }
    }

    void ShowHideInventory(bool show) {
        Button slotXY;
        for (int y = 0; y < slotsY; y++) {
            for (int x = 0; x < slotsX; x++) {
                slotXY = transform.GetChild(x + y * slotsX).GetComponent<Button>();
                slotXY.gameObject.SetActive(show);
            }
        }
    }
}
