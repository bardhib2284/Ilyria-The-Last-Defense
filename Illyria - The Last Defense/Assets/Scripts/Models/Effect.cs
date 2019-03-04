using UnityEngine;
using System.Collections;

public class Effect
{
    public enum TYPE { ACTIVE, PASSIVE }

    public virtual IEnumerator UseEffect(int lengthInRounds, Character character, GameManager gm)
    {
        throw new System.Exception("EFFECT OF THE EFFECT IS NOT OVERRIDEN");
    }
}