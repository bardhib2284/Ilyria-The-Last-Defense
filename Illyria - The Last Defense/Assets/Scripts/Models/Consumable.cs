using UnityEngine;

public class Consumable : ScriptableObject, IConsumable
{
    public int Value;
    public Sprite Icon;

    public Consumable(int value)
    {
        Value = value;
    }

    public virtual void Consume()
    {
        AddValueToManager();
    }

    public virtual void Dispose()
    {
        throw new System.Exception("NOT IMPLEMENTED;");
    }

    public virtual void AddValueToManager()
    {
        throw new System.Exception("NOT IMPLEMENTED;");
    }
}