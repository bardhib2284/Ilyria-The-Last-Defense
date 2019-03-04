using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Items",order = 0)]
public class Item : ScriptableObject
{
    //TODO:ADD THE ITEM PROPERTIES
    public int Health_Bonus;
    public int Attack_Bonus;
    public int Armor_Bonus;
}