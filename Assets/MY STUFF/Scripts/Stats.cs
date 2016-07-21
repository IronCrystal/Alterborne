using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public float strength = 1.0F; //Determines max HP and hp regen
    public float agility = 1.0F; //Determines damage
    public float intelligence = 1.0F; //Determines max mana and mana regen
    public float speed = 10.0F; //Player speed
    public float armor = 0.0F; //Physical Resistance
    public float magicResistance = 1.0F; //Magical Resistance
	public float damageDealt;

	public Class entityClass;

    public int maxHP;
    public int currentHP;

    public int maxMana;
    public int currentMana;

    // Use this for initialization
    void Start () {
		entityClass = Class.Warrior;
        maxHP = (int) strength * 20;
		currentHP = (int) strength * 20;
        maxMana = (int) intelligence * 12;
		currentMana = (int) intelligence * 12;

		if (entityClass == Class.Warrior) {
			damageDealt = strength;
		}
		else if (entityClass == Class.Sorcerer) {
			damageDealt = intelligence;
		}
		else if (entityClass == Class.Rogue) {
			damageDealt = agility;
		}

    }
	
	// Update is called once per frame
	void Update () {
        maxHP = (int)strength * 20;
        maxMana = (int)intelligence * 12;
        if (maxHP <= 0) {
            maxHP = 0;
        }
        if (maxMana <= 0) {
            maxMana = 0;
        }
        if (currentHP >= maxHP) {
            currentHP = maxHP;
        }
        if (currentMana >= maxMana) {
            currentMana = maxMana;
        }
    }

	public enum Class{
		Warrior, Sorcerer, Rogue
	};
}
