using UnityEngine;
using System.Collections;

public class ItemWeapon : Item {

    public int damage; //damage the weapon deals
    public AttackType attack;

    public enum AttackType {
        Wide, //cone attack in front
        Circle, //all around
        Projectile, //fires forward until it hits something
        Forward, //right in front
        Pierce //forward, but pierces through 2 or enemies
	};

	public ItemWeapon(string name, int id, string desc, int dam, AttackType att) : base(name, id, desc, ItemType.Weapon) {
        this.damage = dam;
        this.attack = att;
    }
}
