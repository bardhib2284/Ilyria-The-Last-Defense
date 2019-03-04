using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ConsumableManager : MonoBehaviour
{
    public static ConsumableManager insance;
    public List<Consumable> consumables;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool UseConsumable<T>(T consumable,int value) where T : IConsumable
    {
        Consumable cons = consumables.Find(x => x.Equals(consumable));
        if(cons.Value >= value)
        {
            cons.Value -= value;
            return true;
        }
        else
        {
            //TODO: ADD A DIALOG SAYING YOU DONT HAVE ENOUGH GOLD TO BUY THIS
            Debug.Log("You dont have enough " + cons.name + " to buy the product");
            return false;
        }
    }

}
