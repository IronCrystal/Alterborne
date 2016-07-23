using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {
	Inventory inv;
	Item item;
    string name;
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
            name = item.itemName;
            if (inv.inventory.Count < (inv.slotsX * inv.slotsY)) {
                Destroy(other.gameObject);
			    inv.AddItemToInv(name);
                
            }
        }
		else {
            Debug.Log("Not an item!");
        }
    }
}
