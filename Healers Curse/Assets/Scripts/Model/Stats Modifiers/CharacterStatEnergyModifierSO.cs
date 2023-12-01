using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class CharacterStatEnergyModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        Energy energy = character.GetComponent<Energy>();
        if (energy != null)
        {
            energy.AddEnergy((int)val);
        }
    }

    public override void ReduceCharacter(GameObject character, float val)
    {
        Energy energy = character.GetComponent<Energy>();
        if (energy != null)
        {
            energy.Reduce((int)val);
        }
    }
}
