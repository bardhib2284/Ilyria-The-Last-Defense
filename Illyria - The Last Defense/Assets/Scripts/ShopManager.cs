using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public List<Shop> shops;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnOnTheHeroSummoning()
    {
        foreach (Shop s in shops)
        {
            if (s is HeroShop)
            {
                ((HeroShop)s)?.ShowHeroUI();
                break;
            }
        }
    }
}
