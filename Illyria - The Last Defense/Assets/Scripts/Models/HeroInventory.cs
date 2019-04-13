using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroInventory : Inventory
{
    public GameObject Hero_UI;
    public List<CharacterJson> allCharacters => DBTestBehaviourScript.instance.ReadCharacters();

    private void Start()
    {
        if(Hero_UI == null)
        {
            Hero_UI = Resources.Load<GameObject>("UI/Hero_UI");
        }
        //allCharacters = FindObjectOfType<TeamManager>().GetCharacters();
    }

    public override void ShowInventoryUI()
    {
        this.gameObject.SetActive(true);

        ListAllCharacters();
    }

    public override void HideInventoryUI()
    {
        this.gameObject.SetActive(false);
    }

    private void ListAllCharacters()
    {
        //TODO: USE THE TEAM MANAGER FOR THIS
        Transform content = this.transform.GetChild(0).GetChild(0).GetChild(0);
        List<CharacterJson> characters = new List<CharacterJson>(allCharacters.OrderByDescending(c => c.Current_Level).ToList());
        foreach (var c in characters)
        {
            GameObject heroTemplateUI = Instantiate(Hero_UI, content);
            Transform gChild = heroTemplateUI.transform.GetChild(0);
            gChild.GetComponent<Image>().sprite = Resources.Load<Character>("HeroPrefabs/" + c.Name).CharacterIcon;
            heroTemplateUI.transform.GetChild(1).GetComponent<Text>().text = c.Name;

            for (int i = 0; i < (int)c.Stars; i++)
            {
                Instantiate(Resources.Load<GameObject>("UI/Star"), gChild.GetChild(0));
            }
            gChild.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("FactionIcons/" +Resources.Load<Character>("HeroPrefabs/" + c.Name).Faction);
            gChild.GetChild(2).GetComponent<TextMeshProUGUI>().text = c.Current_Level.ToString();
        }
    }
    private void OnDisable()
    {
        Transform content = this.transform.GetChild(0).GetChild(0).GetChild(0);
        if (content.childCount > 0)
        {
            for (int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }
        }
    }
}