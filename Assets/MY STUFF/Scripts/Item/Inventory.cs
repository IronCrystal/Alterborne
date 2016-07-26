using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public int slotsX, slotsY;
    public ItemDatabase database;

    public Item[,] inventory; //2D array of Items
    int itemCount; //total number of Items in inventory
    private bool showInventory;

    public Button slot;
    Item item;
    Sprite icon;

	void Start() {    
        inventory = new Item[slotsX, slotsY]; //initialize array
        itemCount = 0;
		AddItemToInv (0); //add items to array
        AddItemToInv (1);
        DrawInventoryGUI(); //draw Inventory (starts disabled though)
	}

	public bool AddItemToInv(int id){ //ads an item into the inventory using the id of the item. Returns true if item added sucessfully
		item = database.getData(id); //get information on the item from the database
        for (int y = 0; y < slotsY; y++) //iterate through the whole array to find an empty space (horribly inefficient, will refactor later)
        {
            for (int x = 0; x < slotsX; x++)
            {
                if (inventory[x, y] == null) //if empty space
                {
                    inventory[x, y] = item; //add item
                    itemCount++; //increment itemCount
                    return true;
                }
            }
        }
        return false; //return flase if inventory full
	}
		
    public void TurnOnOffInventory() { //disables/enables inventory
        showInventory = !showInventory; //switch bool
        ShowHideInventory();
    }

    public void DrawInventoryGUI() //creates the Inventory GUI
    {
        for (int c = 0;  c < transform.childCount; c++) {
            Destroy(transform.GetChild(c).gameObject); //destroy any existing slots
        }

        int count = 0; //current item we're on
        Button slotXY; // current button
        int invX = 0; //used to access inventory
        int invY = 0;

        for (int y = slotsY - 1; y >= 0; y--) //iterate through list from top to bottom
        {
            for (int x = 0; x < slotsX; x++) //left to right
            {
                slotXY = Instantiate(slot, new Vector3(x * 40 + 40, y * 40 - 50 * slotsY, 0), transform.rotation) as Button; //create the slot
                icon = slotXY.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite; //get the sprite of the slot's child
                if (count < itemCount) //if there's an item
                {                    
                    icon = inventory[invX, invY].itemIcon; //set it to be the item's icon
                    slotXY.gameObject.SetActive(showInventory); //turn on/off slots depending on if the inventory is currently open               
                    slotXY.transform.SetParent(transform, false); //set the parent to be Inventory but don't let it affect the transform
                    count++; //increment count
                }
                else //if no more items in inventory
                {
                    icon = slotXY.gameObject.GetComponent<Image>().sprite; //set sprite to be a slot sprite
                    slotXY.gameObject.SetActive(showInventory);
                    slotXY.transform.SetParent(transform, false);
                }
                invX++; //increment invX (really just the same as x)
            }           
            invY++; //opposite of y so that we access inventory elements correctly
            invX = 0; //reset invX
        }
    }

    void ShowHideInventory() { //disables/enables inventory
        Button slotXY;
        for (int y = 0; y < slotsY; y++) { //go through all buttons
            for (int x = 0; x < slotsX; x++) {
                slotXY = transform.GetChild(x + y * slotsX).GetComponent<Button>(); 
                slotXY.gameObject.SetActive(showInventory); //disable/enable
            }
        }
    }
}
