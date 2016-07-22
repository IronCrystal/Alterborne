using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {
	Inventory inv;
	Item item;
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
		inv = transform.GetChild(1).GetComponent<Inventory>();
		item = other.GetComponent<Item> ();
			/*if (item.itemID == 1) {
				potion20 = 
			}*/
            if (inv.inventory.Count < (inv.slotsX * inv.slotsY)) {
				inv.AddItemToInv(item.itemName);
                Destroy(other.gameObject);
            }
        }
		else {
            Debug.Log("Not an item!");
        }
    }
}
