using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinosAttack : MonoBehaviour {

    public float punchDamage = 25;

    void DamagePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<DamageableEntity>().OnDamage(gameObject, punchDamage);
        }
    }
}
