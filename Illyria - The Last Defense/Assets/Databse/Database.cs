using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using Newtonsoft.Json;
using System.Collections.Generic;

public class Database : MonoBehaviour
{
    public const string All_Characters_Table_Path = "Database/Table_Characters";
    public Table All_Characters_Table ;
    public const string All_Characters_File_Name = "All_Characters";

    private void Start()
    {
        All_Characters_Table = Resources.Load<Table>(All_Characters_Table_Path);
        Debug.Log(All_Characters_Table);
        All_Characters_Table.content = ReadAllCharacters();
        if(All_Characters_Table.content != null)
        {
            foreach (var cJ in All_Characters_Table.content)
            {
                Debug.Log(cJ);
            }
        }
    }

    private void OnApplicationQuit()
    {
        WriteAllCharacters();
    }

    private List<CharacterJson> ReadAllCharacters()
    {
        string path = Application.persistentDataPath + "/" + All_Characters_File_Name + ".txt";
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

    private void WriteAllCharacters()
    {
        string path = Application.persistentDataPath + "/" + All_Characters_File_Name + ".txt";
        using (FileStream fs = new FileStream(@path
                                     , FileMode.OpenOrCreate
                                     , FileAccess.ReadWrite))
        {
            StreamWriter tw = new StreamWriter(fs);
            if(All_Characters_Table.content != null || All_Characters_Table.content.Count > 0)
            {
                string contentToWrite = JsonConvert.SerializeObject(All_Characters_Table.content);
                tw.Write(contentToWrite);
                tw.Flush();
            }
        }
    }
}