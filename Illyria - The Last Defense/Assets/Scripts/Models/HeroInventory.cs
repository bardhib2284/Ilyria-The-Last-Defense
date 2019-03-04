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
    public List<CharacterJson> allCharacters = new List<CharacterJson>();
    private void Start()
    {
        if(Hero_UI == null)
        {
            Hero_UI = Resources.Load<GameObject>("UI/Hero_UI");
        }
        allCharacters = FindObjectOfType<TeamManager>().GetCharacters();
    }

    public override void ShowInventoryUI()
    {
        this.gameObject.SetActive(true);
        this.gameObject.SetActive(true);
        this.gameObject.SetActive(true);

        ListAllCharacters();
    }

    public override void HideInventoryUI()
    {
        this.gameObject.SetActive(false);
    }
    private void ListAllCharacters()
    {
        allCharacters = FindObjectOfType<TeamManager>().GetCharacters();
        Transform content = this.transform.GetChild(0).GetChild(0).GetChild(0);
        allCharacters = allCharacters.OrderByDescending(c => c.Current_Level).ToList();
        foreach (var c in allCharacters)
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
}