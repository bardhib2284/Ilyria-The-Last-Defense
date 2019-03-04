using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class DamageManager
{
    public static (int,bool) CalculateDamage(Character attacker, Character receiver)
    {
        int damage = attacker.Attack_Current;
        bool didCrit = false;
        if (!attacker.Has_True_Hit)
        {
            Debug.Log("Attacker doesnt have true hit so checking block");

            if (receiver.Block_Percentage >= 1)
            {
                Debug.Log("Receiver Has Block : " + receiver.Block_Percentage);
                bool blocked = Random.Range(0, 100) < receiver.Block_Percentage;
                Debug.Log(blocked == true ? "Receiver Blocked The Damage " : "Receiver Did Not Block The Damage ");
                if (blocked) { return (0,false); }
            }
            Debug.Log("Receiver Does Not Have Block");
        }
        else { Debug.Log("Attacker has true hit so ignoring block"); }

        if (attacker.Critical_Percentage > 1)
        {
            Debug.Log("Attacker has critical chance");
            int randomPercentageNumber = Random.Range(0, 100);
            bool crit = randomPercentageNumber <= attacker.Critical_Percentage;
            Debug.Log(crit == true ? "Attacker Did Crit Because" + randomPercentageNumber + " is smaller than the critical percentage" + attacker.Critical_Percentage : "Attacker Did Not Crit Because" + randomPercentageNumber + " is bigger than the critical percentage" + attacker.Critical_Percentage);
            if (crit)
            {
                damage += (damage * attacker.Critical_Damage_Percentage) / 100;
                didCrit = true;
            }
        }

        if (receiver.Damage_Reduction >= 1)
        {
            damage -= ((damage * receiver.Damage_Reduction) / 100);
        }

        switch (attacker.Faction)
        {
            case Character.CharacterFaction.Dark:
                switch (receiver.Faction)
                {
                    case Character.CharacterFaction.Light:
                        damage = (int)(damage * 1.5);
                        break;
                }
                break;
            case Character.CharacterFaction.Chaos:
                switch (receiver.Faction)
                {
                    case Character.CharacterFaction.Water:
                        damage = (int)(damage * 1.5);
                        break;
                }
                break;
            case Character.CharacterFaction.Water:
                switch (receiver.Faction)
                {
                    case Character.CharacterFaction.Fire:
                        damage = (int)(damage * 1.5); break;
                }
                break;
            case Character.CharacterFaction.Light:
                switch (receiver.Faction)
                {
                    case Character.CharacterFaction.Dark:
                        damage = (int)(damage * 1.5); break;
                }
                break;
        }

        Debug.LogError("ARMOUR REDUCED " + damage * (int)(receiver.Armor_Current * 0.69) / 100 + " damage ");
        damage -= (damage * (int)(receiver.Armor_Current * 0.69)) / 100;
        return (damage,didCrit);
    }
}