using System.Collections.ObjectModel;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class CharacterJson 
{
    public int Id;
    public string Name;
    public int Stars;
    public ObservableCollection<Item> Items;
    public int Current_Level;

    public override string ToString()
    {
        return Name + Id + Stars +  Items + Current_Level;
    }
}