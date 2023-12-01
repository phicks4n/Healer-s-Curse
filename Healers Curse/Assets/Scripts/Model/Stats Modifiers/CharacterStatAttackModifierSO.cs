using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatAttackModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        Attack attack = character.GetComponent<Attack>();
        if (attack != null)
        {
            attack.AddAttack((int)val);
        }
    }

    public override void ReduceCharacter(GameObject character, float val)
    {
        Attack attack = character.GetComponent<Attack>();
        if (attack != null)
        {
            attack.Reduce((int)val);
        }
    }
}
