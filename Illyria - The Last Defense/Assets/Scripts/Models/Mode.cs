using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Mode",menuName = "Modes",order =2)]
public class Mode : ScriptableObject
{
    public string Mode_Name;
    public Character.CharacterStars Mode_Character_Enemies_Stars;
    public List<Character> Mode_Characters_Enemies;
    public Reward Rewards;
}