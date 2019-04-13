using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public List<CharacterJson> Characters;

    private void Start()
    {
        StartCoroutine("GetCharactersFromDatabase");
    }

    public void AddANewCharacterToTheTeam(Character c)
    {
        DBTestBehaviourScript.instance.AddCharacter(c);
    }

    public IEnumerator GetCharactersFromDatabase()
    {
        yield return Characters = DBTestBehaviourScript.instance.ReadCharacters();
    }
}