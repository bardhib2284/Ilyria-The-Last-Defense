using UnityEngine;
using System.Collections;

public class Gem : Consumable
{
    public Gem()
    {
    }

    public Gem(int value) : base(value)
    {
    }

    public Gem(int id, string name, int value, string icon) : base(id, name, value, icon)
    {
    }

    public override void AddToInventory()
    {
        ConsumableManager.instance.AddConsumable(this as Gem);
    }

    public override void Dispose()
    {
        base.Dispose();
    }

    public override bool Equals(object other)
    {
        if(other is Consumable)
        {
            if(other.GetType() == typeof(Gem))
            {
                return true;
            }
            return false;
        }
        return false;
    }
}
