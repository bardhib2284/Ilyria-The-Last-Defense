using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public static Table characters_Table; // => Database.GetCharacters

    private void Start()
    {
        characters_Table = Resources.Load<Table>(Database.All_Characters_Table_Path);
    }
    private List<CharacterJson> Characters;

    public void AddANewCharacterToTheTeam(CharacterJson c)
    {
        Debug.Log(characters_Table);
        Debug.Log(c);
        if(characters_Table.content == null)
        {
            characters_Table.content = new List<CharacterJson>();
        }
        characters_Table.content.Add(c);
        Debug.Log(characters_Table.content.Count);
    }

    public List<CharacterJson> GetCharacters()
    {
        Debug.Log("Getting Characters");
        return FindObjectOfType<Database>().All_Characters_Table.content;
    }
}