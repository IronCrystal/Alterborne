using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public float strength = 1.0F; //Determines max HP and hp regen
    public float agility = 1.0F; //Determines damage
    public float intelligence = 1.0F; //Determines max mana and mana regen
    public float speed = 10.0F; //Player speed
    public float armor = 0.0F; //Physical Resistance
    public float magicResistance = 1.0F; //Magical Resistance

    public int maxHP = 100;
    public int currentHP = 100;

    public int maxMana = 100;
    public int currentMana = 100;

    // Use this for initialization
    void Start () {
        maxHP = (int) strength * 20;
        maxMana = (int) intelligence * 12;
    }
	
	// Update is called once per frame
	void Update () {
        maxHP = (int)strength * 20;
        maxMana = (int)intelligence * 12;
        if (maxHP < 0) {
            maxHP = 0;
        }
        if (maxMana < 0) {
            maxMana = 0;
        }
        if (currentHP > maxHP) {
            currentHP = maxHP;
        }
        if (currentMana > maxMana) {
            currentMana = maxMana;
        }
    }
}
