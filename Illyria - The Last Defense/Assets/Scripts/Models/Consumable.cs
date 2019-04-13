using UnityEngine;

[System.Serializable]
public class Consumable : IConsumable
{
    public int Id;
    public string Name;
    public int Value;
    public string Icon;

    public Consumable()
    {

    }
    public Consumable(int id,string name,int value,string icon)
    {
        Id = id;
        Name = name;
        Value = value;
        Icon = icon;
    }

    public Consumable(int value)
    {
        Value = value;
    }

    public virtual void Consume()
    {
        AddToInventory();
    }

    public virtual void Dispose()
    {
        throw new System.Exception("NOT IMPLEMENTED;");
    }

    public virtual void AddToInventory()
    {
        ConsumableInventory.instance.AddConsumable(this);
    }

}