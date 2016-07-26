using UnityEngine;
using System.Collections;

public class ItemConsumable : Item
{

    public int amount;
    public HealType healType;

    public enum HealType
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
