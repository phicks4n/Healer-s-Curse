using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class CharacterStatMagicModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        Magic magic = character.GetComponent<Magic>();
        if (magic != null)
        {
            magic.AddMagic((int)val);
        }
    }

    public override void ReduceCharacter(GameObject character, float val)
    {
        Magic magic = character.GetComponent<Magic>();
        if (magic != null)
        {
            magic.Reduce((int)val);
        }
    }
}
