using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine.UI;
using System.Linq;

public class ConsumableManager : MonoBehaviour
{
    public static ConsumableManager instance;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);

    }

    // Update is called once per frame,
    void Update()
    {
        
    }

    public void AddConsumable(Consumable c)
    {

    }

    private void OnApplicationQuit()
    {
        //Database.instance.WriteConsumables(consumables);
    }

    public bool UseConsumable<T>(T consumable, int value) where T : IConsumable
    {
        if(consumable is Consumable)
        {
            Consumable c = consumable as Consumable;
            Consumable cons = ConsumableInventory.instance.AllConsumables.FirstOrDefault(x => x.Name == c.Name.ToUpper());
            if (cons != null)
            {
                Debug.Log(cons.Value + value);
                if (cons.Value >= value)
                {
                    cons.Value -= value;
                    ConsumableInventory.instance.UpdateResource(cons);
                    return true;
                }
                Dialog.instance.CreateAlertDialog("You Don't Have Enough " + cons.Name,"Ok");
                return false;
            }
            Dialog.instance.CreateAlertDialog("You Don't Own Any " + c.Name, "Ok");
            return false;
        }
        Dialog.instance.CreateAlertDialog("WARNING: USE CONSUMABLE WITH A PARAMETER OF NON CONSUMABLE","OKOK");
        return false;

    }

}