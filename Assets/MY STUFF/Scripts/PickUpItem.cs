using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {

    public Item item;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collision entered!");
        if (item != null) {
            if (other.gameObject.tag == "Player") {
                Inventory inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
                if (inv.inventory.Count < (inv.slotsX * inv.slotsY)) {
                    inv.AddItem(item);
                    Destroy(gameObject);
                }
            }else {
                Debug.Log("Not a player!");
            }
        }
    }
}
