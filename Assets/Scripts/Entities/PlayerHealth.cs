using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : DamageableEntity {

    public AnimationsManager animationsManager;

    public override bool OnDamage(GameObject origin, float damage, float delayDeath = 0)
    {
        bool damaged = base.OnDamage(origin, damage, delayDeath);
        if (damaged)
        {
            animationsManager.getHit = true;
        }
        return damaged;
    }
}
