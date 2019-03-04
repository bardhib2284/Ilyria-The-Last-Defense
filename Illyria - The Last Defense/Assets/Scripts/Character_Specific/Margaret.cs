using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Margaret : Character
{
    public Margaret(CharacterJson characterJson) : base(characterJson)
    {
    }

    public override void Attack()
    {
        throw new NotImplementedException();
    }

    public override void StartTurn()
    {
        throw new NotImplementedException();
    }

    public override void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }

    public override IEnumerator UpdateHealthUI()
    {
        throw new NotImplementedException();
    }

    public override void UpdateManaUI(int manaToRefill)
    {
        base.UpdateManaUI(manaToRefill);
    }
}