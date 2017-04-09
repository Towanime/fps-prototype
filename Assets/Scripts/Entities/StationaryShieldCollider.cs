using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryShieldCollider : DamageableEntity {

    public DamageableEntity shiedDamageableEntity;

    public override bool OnDamage(GameObject origin, float damage, float delayDeath = 0)
    {
        return shiedDamageableEntity.OnDamage(origin, damage, delayDeath);
    }
}
