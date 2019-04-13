using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System;
using System.Threading.Tasks;

//TODO: 
public class Database : MonoBehaviour
{
    public static Database instance;
    public const string ALL_CHARACTERS_FILE_NAME = "All_Characters";
    public const string ALL_CONSUMABLES_FILE_NAME = "All_Consumables";
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private List<CharacterJson> ReadAllCharacters()
    {
        string path = Application.persistentDataPath + "/" + ALL_CHARACTERS_FILE_NAME + ".txt";
        using (FileStream fs = new FileStream(@path
                                     , FileMode.OpenOrCreate
                                     , FileAccess.ReadWrite))
        {
            StreamReader tw = new StreamReader(fs);
            string content = tw.ReadToEnd();
            List<CharacterJson> characters = JsonConvert.DeserializeObject<List<CharacterJson>>(content);
            tw.Close();
            return characters;
        }
    }
}