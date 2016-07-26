using UnityEngine;
using System.Collections;

public class ItemConsumable : Item
{

    public int amount; //amount it heals (could be negative)
    public HealType healType; //what it heals

    public enum HealType //could include effects here
    {
        HP,
        Mana,
        Stamina
    };

    public ItemConsumable(string name, int id, string desc, int am, HealType heal)
        : base(name, id, desc, ItemType.Consumable)
        {
            this.amount = am;
            this.healType = heal;
        }
}
