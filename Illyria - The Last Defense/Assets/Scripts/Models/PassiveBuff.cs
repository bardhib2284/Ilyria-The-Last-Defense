using System.Collections;
using UnityEngine;

public class PassiveBuff : Effect
{
    public bool hasHpBuff;
    public bool hasAttackBuff;
    public bool hasArmorBuff;
    public int valueForHp;
    public int valueForAttack;
    public int valueForArmor;

    public override IEnumerator UseEffect(int lengthInRounds, Character character, GameManager gm)
    {
        int currentRound = gm.round;
        lengthInRounds += currentRound;

        if(hasHpBuff)
        {
            character.Health_Current = (character.Health_Current * valueForHp) / 100;
        }
        if (hasAttackBuff)
        {
            character.Attack_Current = (character.Attack_Current * valueForAttack) / 100;
        }
        if(hasArmorBuff)
        {
            character.Armor_Current = (character.Armor_Current * valueForArmor) / 100;
        }
        yield return new WaitForSeconds(1f);
    }
}