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
		    this.item = other.GetComponent<GroundItem> ().item;
            id = item.itemID;
            if (inv.AddItemToInv(id)){
                Destroy(other.gameObject);			    
                inv.DrawInventoryGUI();                
            }
            else
            {
                Debug.Log("not enough room!");
            }
        }
		else {
            Debug.Log("Not an item!");
        }
    }
}
