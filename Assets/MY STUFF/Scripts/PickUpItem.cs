using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {
	public Inventory inv;
	Item item;
    int id;
	//PotionHP potion20;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collision entered!");
        if (other.tag == "Item") {
		    //inv = transform.GetChild(0).GetComponent<Inventory>();
		    item = other.GetComponent<Item> ();
            id = item.itemID;
            if (inv.inventory.Count < (inv.slotsX * inv.slotsY)) {
                Destroy(other.gameObject);
			    inv.AddItemToInv(id);
                inv.InitInventoryGUI();
                
            }
        }
		else {
            Debug.Log("Not an item!");
        }
    }
}
