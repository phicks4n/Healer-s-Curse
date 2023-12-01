using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class CharacterStatMagicResistModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        MagicResist resist = character.GetComponent<MagicResist>();
        if (resist != null)
        {
            resist.AddResist((int)val);
        }
    }

    public override void ReduceCharacter(GameObject character, float val)
    {
        MagicResist resist = character.GetComponent<MagicResist>();
        if (resist != null)
        {
            resist.Reduce((int)val);
        }
    }
}
