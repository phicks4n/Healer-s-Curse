using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class CharacterStatDodgeChanceModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        DodgeChance dodge = character.GetComponent<DodgeChance>();
        if (dodge != null)
        {
            dodge.AddDodge((int)val);
        }
    }
}
