using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public int slotsX, slotsY;
    public List<Item> inventory = new List<Item>();
    //public List<Texture2D> slots = new List<Texture2D>();
    public GUISkin skin;
    private bool showInventory;
	public Dictionary<int, Item> database = new Dictionary<int, Item>();

	public Sword swordPrefab;
	public PotionHP potionHPPrefab;
    public Button slot;

	void Start() {
        //Sword sword = new Sword();
        //Debug.Log(sword);
        // sword1 = Instantiate(swordPrefab);
        //Debug.Log(sword1);

        AddItemToDB (0, new Sword());
		AddItemToDB (1, new PotionHP());
        
		//AddItemToInv ("Master Sword");
		//AddItemToInv ("HP Potion");

        InitInventoryGUI();
	}

	private void AddItemToDB(int id, Item item){
		database [id] = item;
	}

	public void AddItemToInv(int id){
		Item item = database[id];
		inventory.Add (item);
	}
		
    public void TurnOnOffInventory() {
        showInventory = !showInventory;
        ShowHideInventory();
    }

    public void InitInventoryGUI()
    {
        for (int c = 0;  c < transform.childCount; c++) {
            Destroy(transform.GetChild(c).gameObject);
        }

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
                    slotXY.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = inventory[invX + invY*slotsX].itemIcon;
                    slotXY.gameObject.SetActive(showInventory);
                    
                    slotXY.transform.SetParent(transform, false);
                    count++;
                }
                else
                {
                    slotXY = Instantiate(slot, new Vector3(x * 40 + 40, y * 40 - 50 * slotsY, 0), transform.rotation) as Button;
                    slotXY.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = slotXY.gameObject.GetComponent<Image>().sprite;
                    slotXY.gameObject.SetActive(showInventory);
                    slotXY.transform.SetParent(transform, false);
                }
                invX++;
            }           
            invY++;
            invX = 0;
        }
    }

    void ShowHideInventory() {
        Button slotXY;
        for (int y = 0; y < slotsY; y++) {
            for (int x = 0; x < slotsX; x++) {
                slotXY = transform.GetChild(x + y * slotsX).GetComponent<Button>();
                slotXY.gameObject.SetActive(showInventory);
            }
        }
    }
}
