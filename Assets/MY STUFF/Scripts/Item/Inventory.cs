using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public int slotsX, slotsY;
    public ItemDatabase database;

    public Item[,] inventory;
    int itemCount;
    //public List<Texture2D> slots = new List<Texture2D>();
    public GUISkin skin;
    private bool showInventory;

    public Button slot;
    Item item;

	void Start() {    
        inventory = new Item[slotsX, slotsY];
        itemCount = 0;
		AddItemToInv (0);
        AddItemToInv(1);
        DrawInventoryGUI();
	}

	public bool AddItemToInv(int id){
		item = database.getData(id);
        for (int y = 0; y < slotsY; y++)
        {
            for (int x = 0; x < slotsX; x++)
            {
                if (inventory[x, y] == null)
                {
                    inventory[x, y] = item;
                    itemCount++;
                    return true;
                }
            }
        }
        return false;
	}
		
    public void TurnOnOffInventory() {
        showInventory = !showInventory;
        ShowHideInventory();
    }

    public void DrawInventoryGUI()
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
                if (count < itemCount)
                {
                    slotXY = Instantiate(slot, new Vector3(x * 40 + 40, y * 40 - 50 * slotsY, 0), transform.rotation) as Button;
                    slotXY.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = inventory[invX, invY].itemIcon;
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
