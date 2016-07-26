using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundItemManager : MonoBehaviour {

    public ItemDatabase database;
    public GroundItem groundItem; //groundItem prefab
    public int itemCount; //total number of items
   
    GroundItem newGroundItem;//to be instantiated

    float randX; //decides the position of the items
    float randY;
    int randItem; //decides the item
    
	// Use this for initialization
	void Start () {              
        for (int g = 0; g < itemCount; g++) //repeat for g items
        {
            randX = Random.Range(-11.0f, 11.0f); //range of our current room
            randY = Random.Range(-4.0f, 4.0f);
            randItem = Random.Range(0, 2); //only 2 items exist so far

            newGroundItem = Instantiate(groundItem, new Vector3(randX, randY, 0), transform.rotation) as GroundItem; //create a new ground item
            newGroundItem.item = database.getData(randItem); //set the item variable form data in database
            newGroundItem.transform.SetParent(transform); //set the groundItem's parent to the GroundItems Managaer
            newGroundItem.transform.localScale = new Vector3(0.25f, 0.25f, 1); //scale it down (actual sprites are really big right now)
        }
	}
}
