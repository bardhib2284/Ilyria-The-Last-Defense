using UnityEngine;
using System.Linq;

public class Gold : Consumable
{
    public Gold(int value) : base(value)
    {
    }

    public Gold()
    {
    }

    public Gold(int id, string name, int value, string icon) : base(id, name, value, icon)
    {
    }

    public override void Consume()
    {
        AddToInventory();
    }

    public override void Dispose()
    {

    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override void AddToInventory()
    {
        ConsumableManager.instance.AddConsumable(this as Gold);
    }

    public override bool Equals(object other)
    {
        if (other is Consumable)
        {
            if (other.GetType() == typeof(Gold))
            {
                return true;
            }
            return false;
        }
        return false;
    }
}