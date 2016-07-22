using UnityEngine;
using System.Collections;

public class Sword: Item {
	char attackType;
	int damage;
	public Sword () : base ("Master Sword", 0, "Link's Master Sword", ItemType.Weapon) {
		attackType = 'C';
		damage = 2;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
