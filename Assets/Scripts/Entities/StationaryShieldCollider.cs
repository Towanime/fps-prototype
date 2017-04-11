using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryShieldCollider : DamageableEntity {

    public DamageableEntity shiedDamageableEntity;

    void OnTriggerEnter(Collider other)
    {
        EnemyScript enemyScript = other.GetComponent<EnemyScript>();
        if (enemyScript != null && enemyScript.EnemyCode == 2)
        {
            OnDamage(other.gameObject, shiedDamageableEntity.life);
        }
    }

    public override bool OnDamage(GameObject origin, float damage, float delayDeath = 0)
    {
        return shiedDamageableEntity.OnDamage(origin, damage, delayDeath);
    }
}
