using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int round = 0;
    //TODO : THIS IS UGLY AS SHIT , FIX AS SOON AS POSSIBLE
    // FIX : SEND THE CHARACTERS THROUGH ANOTHER SCRIPT FOR THE PLAYER AND GET THE CHARACTERS FOR THE ENEMY FROM THE CAMPAIGNS
    [Space]
    [Header("All Characters PROPERTIES")]
    public List<Character> playerCharacters;
    [Header("MODE PROPERTIES")]
    [Tooltip("Mode : Everything that is fightable is a mode")]
    public Mode Active_Mode;
    //TODO: THIS IS USED ONLY FOR GRAPHICAL ISSUES, DELETE AND USE THE ACTIVE MODE ENEMIES
    public List<Character> modeCharacters;
    [Space]
    [Header("All Characters PROPERTIES")]
    [Tooltip("This Array Is Used For Getting All The Characters And Sorting Based On Speed Since Speed Prioritizes Attack")]
    [SerializeField]
    private List<Character> allCharacters;
    public List<GameObject> instantiatedGameObjects;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance != null)
        {
            instance = this;
        }


    }
    private void Start()
    {
        Init();
        //Invoke("SaveCharacters",0f);
    }

    //ROUND LOGIC
    public IEnumerator StartRound()
    {
        while (round < 15)
        {
            Debug.Log("ROUND " + round + " STARTED WITH : " + allCharacters.Count + " CHARACTERS LEFT");
            foreach (var character in allCharacters)
            {
                if (!character.Dead && !character.Stunned)
                {
                    if (character.tag == "Left")
                    {
                        character.enemyTeam = allCharacters.FindAll((Character obj) => obj.gameObject.tag == "Right");
                        character.enemyTeam = new List<Character>(character.enemyTeam.FindAll((Character obj) => obj.Dead == false));
                        Debug.LogError("DWDWDWDWDWDWD");
                        character.StartTurn();
                        yield return new WaitUntil(() => character.IsBusy == false);
                    }
                    else if (character.tag == "Right")
                    {
                        character.enemyTeam = allCharacters.FindAll((Character obj) => obj.gameObject.tag == "Left");
                        character.enemyTeam = new List<Character>(character.enemyTeam.FindAll((Character obj) => obj.Dead == false));
                        Debug.LogError("DWDWDWDWDWDWD");
                        character.StartTurn();
                        yield return new WaitUntil(() => character.IsBusy == false);

                        //yield return new WaitUntil(() => character.IsBusy = false);
                    }
                }

            }
            allCharacters = allCharacters.FindAll((i) => i.GetDead() == false);
            round++;
        }
        //Active_Mode.Rewards.ReceiveReward();
    }

    public void Init()
    {
        allCharacters = new List<Character>();
        instantiatedGameObjects = new List<GameObject>();
        if(Active_Mode != null)
            modeCharacters = Active_Mode.Mode_Characters_Enemies;
        Transform holder = FindObjectOfType<PositionHolder>().transform;
        int index = 0;
        foreach (var c in playerCharacters)
        {
            if (holder != null)
            {
                GameObject temp = Instantiate(Resources.Load<GameObject>("HeroPrefabs/" + c.name), holder.GetChild(0).GetChild(index++));
                Character character = temp.GetComponent<Character>();
                character.Speed_Max = 12;
                //character.UpdateLevelStats();
                instantiatedGameObjects.Add(temp);
                temp.tag = "Left";
                allCharacters.Add(character);
            }
        }
        index = 0;
        foreach (var c in modeCharacters)
        {
            if (holder != null)
            {
                GameObject temp = Instantiate(Resources.Load<GameObject>("HeroPrefabs/" + c.name), holder.GetChild(1).GetChild(index++));
                Character character = temp.GetComponent<Character>();
                character.Speed_Max = 24;
                //character.UpdateLevelStats();
                instantiatedGameObjects.Add(temp);
                temp.tag = "Right";
                allCharacters.Add(temp.GetComponent<Character>());
            }
        }
        Debug.Log(allCharacters.Count);
        instantiatedGameObjects = new List<GameObject>(instantiatedGameObjects.OrderByDescending(i => i.GetComponent<Character>().Speed_Max));
        allCharacters = new List<Character>(allCharacters.OrderByDescending(i => i.Speed_Max));
        StartCoroutine("StartRound");
    }

    public async void SaveCharacters()
    {
        //await DatabaseManager.WriteToCharacters(JsonConvert.SerializeObject(modeCharacters));
    }
}