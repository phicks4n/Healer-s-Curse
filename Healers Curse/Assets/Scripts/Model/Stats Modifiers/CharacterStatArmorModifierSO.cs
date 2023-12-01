using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class CharacterStatArmorModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        Armor armor = character.GetComponent<Armor>();
        if (armor != null)
        {
            armor.AddArmor((int)val);
        }
    }

    public override void ReduceCharacter(GameObject character, float val)
    {
        Armor armor = character.GetComponent<Armor>();
        if (armor != null)
        {
            armor.Reduce((int)val);
        }
    }
}
