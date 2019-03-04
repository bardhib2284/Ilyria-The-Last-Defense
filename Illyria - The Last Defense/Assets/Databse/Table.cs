using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName ="Table_Name",menuName = "Database/New Table",order =1)]
public class Table : ScriptableObject 
{
    public List<CharacterJson> content;
}