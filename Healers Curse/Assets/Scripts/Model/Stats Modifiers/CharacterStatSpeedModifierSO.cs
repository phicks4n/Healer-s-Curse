using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class CharacterStatSpeedModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        Speed speed = character.GetComponent<Speed>();
        if (speed != null)
        {
            speed.AddSpeed((int)val);
        }
    }
}
