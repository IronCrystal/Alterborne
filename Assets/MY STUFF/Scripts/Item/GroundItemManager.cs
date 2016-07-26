using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundItemManager : MonoBehaviour {
    public ItemDatabase database;
    public GroundItem groundItem;
    public int itemCount;

    GroundItem[] allGroundItems;    
    GroundItem newGroundItem;

    float randX;
    float randY;
    int randItem;
    //public GroundItem 
	// Use this for initialization
	void Start () {        
        allGroundItems = new GroundItem[itemCount];
        for (int g = 0; g < itemCount; g++)
        {
            randX = Random.Range(-11.0f, 11.0f);
            randY = Random.Range(-4.0f, 4.0f);
            randItem = Random.Range(0, 2);

            newGroundItem = Instantiate(groundItem, new Vector3(randX, randY, 0), transform.rotation) as GroundItem;
            newGroundItem.item = database.getData(randItem);
            newGroundItem.transform.SetParent(transform);
            newGroundItem.transform.localScale = new Vector3(0.25f, 0.25f, 1);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
