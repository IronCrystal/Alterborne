using UnityEngine;
using System.Collections;

public class GroundItem : MonoBehaviour {
    public Item item;
	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = item.itemIcon;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
