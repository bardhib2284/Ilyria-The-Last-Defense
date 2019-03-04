using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if(instance != null)
        {
            instance = this;
        }
        if(Inventories.Count < 1)
        {
            Debug.LogError("INVENTORIES ARE EMPTY");
        }
    }
    public List<Inventory> Inventories;
    
    //UI METHODS
    public void TurnOnTheHeroInventory()
    {
        foreach(Inventory inventory in Inventories)
        {
            if(inventory is HeroInventory)
            {
                ((HeroInventory)inventory)?.ShowInventoryUI();
                break;
            }
        }
    }
}
