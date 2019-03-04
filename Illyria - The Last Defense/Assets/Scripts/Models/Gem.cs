using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "Gem", menuName = "Consumable/Gem", order = 3)]
public class Gem : Consumable
{
    public Gem(int value) : base(value)
    {
    }

    public override void AddValueToManager()
    {
        Consumable managerGold = FindObjectOfType<ConsumableManager>().consumables.Find(x => x is Gem);
        managerGold.Value += this.Value;
    }

    public override void Consume()
    {
        base.Consume();
    }

    public override void Dispose()
    {
        base.Dispose();
    }

    public override bool Equals(object other)
    {
        if(other is Consumable)
        {
            if(other is Gem)
            {
                return true;
            }
            return false;
        }
        return false;
    }
}
