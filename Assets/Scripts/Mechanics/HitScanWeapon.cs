using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitScanWeapon : Weapon {

    public LayerMask targetLayerMask;
    public LayerMask ignoreLayerMask;

    [Tooltip("Origin of the bullet, its forward is used as the aiming direction.")]
    public Transform aimingTransform;
    [Tooltip("How far the bullet travels.")]
    public float range = 400;
    [Tooltip("Maximum range that the aiming can spread from the center.")]
    public float spreadSize = 0.01f;
    [Tooltip("Character accuracy, 1 is perfect accuracy to the center of the aiming and 0 is completely random within the range of the spread size.")]
    [Range(0f, 1f)]
    public float accuracy = 1;

    void Start()
    {
        currentBulletCount = magazineSize;
    }

    public override bool ShootContinuously()
    {
        if (currentBulletCount <= 0)
        {
            Debug.Log(gameObject + "Needs to reload");
            return false;
        }

        if (!waitingFireRateCooldown)
        {
            Vector3 startPosition = Vector3.zero;
            Vector3 direction = Vector3.Slerp(aimingTransform.forward, Random.onUnitSphere, Mathf.Lerp(spreadSize, 0f, accuracy));

            Debug.Log(gameObject + "Fired bullet from hitscan weapon");
            Debug.DrawRay(aimingTransform.TransformPoint(startPosition), direction.normalized * range, Color.red, 0.5f);

            RaycastHit hit;
            if (Physics.Raycast(aimingTransform.TransformPoint(startPosition), direction, out hit, range, ~ignoreLayerMask))
            {
                GameObject other = hit.transform.gameObject;
                Debug.Log(gameObject + "Bullet from hitscan weapon hit: " + other);
                DamageableEntity damageableEntity;
                if (Util.IsObjectInLayerMask(targetLayerMask, other) && 
                    (damageableEntity = other.GetComponent<DamageableEntity>()) != null)
                {
                    Debug.Log(gameObject + "Bullet from hitscan weapon is trying to damage: " + other);
                    bool damaged = damageableEntity.OnDamage(gameObject, damage);
                    Debug.Log(gameObject + "Result of bullet damage: " + damaged);
                }
            }

            waitingFireRateCooldown = true;
            lastBulletFiredMoment = Time.time;
            SubstractBullet();
            return true;
        }

        return false;
    }

    public int CurrentBulletCount
    {
        get { return currentBulletCount; }
    }
}
