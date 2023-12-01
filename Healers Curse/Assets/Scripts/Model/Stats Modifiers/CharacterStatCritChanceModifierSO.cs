using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class CharacterStatCritChanceModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        CritChance crit = character.GetComponent<CritChance>();
        if (crit != null)
        {
            crit.AddCrit((int)val);
        }
    }

    public override void ReduceCharacter(GameObject character, float val)
    {
        CritChance crit = character.GetComponent<CritChance>();
        if (crit != null)
        {
            crit.Reduce((int)val);
        }
    }
}
