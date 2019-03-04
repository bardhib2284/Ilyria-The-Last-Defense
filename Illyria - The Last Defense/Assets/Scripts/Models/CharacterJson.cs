using System.Collections.ObjectModel;
using UnityEngine;
using Newtonsoft.Json;
public class CharacterJson
{
    public int Id = PlayerPrefs.GetInt("CharacterID") + 1;
    public string Name { get; set; }
    public Character.CharacterStars Stars { get; set; }
    public ObservableCollection<Item> Items { get; set; }
    public int Current_Level { get; set; }

    public override string ToString()
    {
        return Name + Id + Stars +  Items + Current_Level;
    }
}