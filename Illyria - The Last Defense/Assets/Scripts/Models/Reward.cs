using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Rewards_",menuName = "Reward",order =3)]
public class Reward : ScriptableObject, IRewardable
{
    public List<Consumable> consumables;

    public void ReceiveReward()
    {
        foreach(var c in consumables)
        {
            c.Consume();
        }
    }

    public void RewardUI()
    {
        //TODO : IMPLEMENT UI FOR THE REWARDS
        throw new System.NotImplementedException();
    }
}
