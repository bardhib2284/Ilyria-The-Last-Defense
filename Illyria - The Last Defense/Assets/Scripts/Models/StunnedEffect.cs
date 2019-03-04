using UnityEngine;
using System.Collections;

public class StunnedEffect : Effect
{
    public override IEnumerator UseEffect(int lengthInRounds,Character character,GameManager gm)
    {
        Debug.Log("Stunned Effect Applied");
        character.SetStunned();
        Debug.Log(character.Stunned);
        int currentRound = gm.round;
        lengthInRounds += currentRound;
        yield return new WaitWhile(() => lengthInRounds >= gm.round);
        character.Stunned = false;
    }
}
