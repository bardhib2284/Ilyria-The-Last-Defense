using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shop : MonoBehaviour
{
    public string ShopName;
    //TODO: CHANGE THIS LIST TO SOMETHING BETTER
    public List<MonoBehaviour> shopContents;

    public abstract void UseConsumable<T>(T consumable,int value) where T : IConsumable;
}