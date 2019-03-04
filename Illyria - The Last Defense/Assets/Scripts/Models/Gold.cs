using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "Gold", menuName = "Consumable/Gold", order = 3)]
public class Gold : Consumable
{
    public Gold(int value) : base(value)
    {
    }

    public override void Consume()
    {
        AddValueToManager();
    }

    public override void Dispose()
    {

    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override void AddValueToManager()
    {
        Consumable managerGold = FindObjectOfType<ConsumableManager>().consumables.Find(x => x is Gold);
        managerGold.Value += this.Value;
    }

    public override bool Equals(object other)
    {
        if (other is Consumable)
        {
            if (other is Gold)
            {
                return true;
            }
            return false;
        }
        return false;
    }
}
