using UnityEngine;
using System.Collections;

public class PotionHP: Item {
	char healType;
	int recoveredHP;
	public PotionHP () : base("HP Potion", 1, "Refreshing", ItemType.Consumable) {
		healType = 'H';
		recoveredHP = 20;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	
	}
}
