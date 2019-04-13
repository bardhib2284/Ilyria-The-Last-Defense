using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    private void Awake()
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
        if(Inventories.Count < 1)
        {
            Debug.LogError("INVENTORIES ARE EMPTY");
        }

    }
    public List<Inventory> Inventories;
    
    //UI METHODS
    public void CloseMainInventory()
    {
        foreach(var inventory in Inventories)
        {
            inventory.HideInventoryUI();
        }
    }

}