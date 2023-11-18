using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class CharacterStatManaModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        Mana mana = character.GetComponent<Mana>();
        if (mana != null)
        {
            mana.AddMana((int)val);
        }
    }
}
