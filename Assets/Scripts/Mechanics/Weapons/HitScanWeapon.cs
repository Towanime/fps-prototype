using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitScanWeapon : Weapon {

    public LayerMask targetLayerMask;
    public LayerMask ignoreLayerMask;

    [Tooltip("Origin of the bullet, its forward is used as the aiming direction.")]
    public Transform aimingTransform;
    [Tooltip("GameObject that is instanciated on the contact point of the bullet. Optional.")]
    public GameObject bulletContactPointPrefab;
    [Tooltip("How far the bullet travels.")]
    public float range = 400;
    [Tooltip("Minimum and standard range that the aiming can spread from the center.")]
    [Range(0f, 1f)]
    public float minSpreadRange = 0.0005f;
    [Tooltip("Maximum range that the aiming can spread from the center, applied after shooting consecutively for a while.")]
    [Range(0f, 1f)]
    public float maxSpreadRange = 0.012f;
    [Tooltip("Character accuracy, 1 is perfect accuracy to the center of the aiming and 0 is completely random within the range of the spread size.")]
    [Range(0f, 1f)]
    public float accuracy = 1;
    [Tooltip("How the range grows in relation to the time shooting consecutively.")]
    public AnimationCurve spreadRangeGrowthCurve;
    [Tooltip("How much time has to pass shooting consecutively until the spread range is at maximum.")]
    public float timeShootingUntilMaxSpreadRange = 0.8f;
    [Tooltip("Rate at which the spread range decreases automatically each frame. Only applied when shooting.")]
    public float spreadRangeSlowdownRateWhenShooting = 0.5f;
    [Tooltip("Rate at which the spread range decreases automatically each frame. Only applied when not shooting.")]
    public float spreadRangeSlowdownRateWhenNotShooting = 2;

    public AudioSource weaponAudioSource;
    public AudioClip shootSfx;

    /// <summary>
    /// For how long the player has been shooting consecutively, caps at the value of timeShootingUntilMaxSpreadRange.
    /// </summary>
    private float currentConsecutiveShootingTime;
    /// <summary>
    /// True as long as the player hasn't stopped the shooting action.
    /// </summary>
    private bool consecutiveShooting;

    [Header("Camera Recoil Settings")]
    public bool useCameraRecoil;
    public float recoilTime = 0.08f;
    public float fovModifier = -0.4f;
    public Camera playerCamera;
    public AnimationCurve recoilAnimationCurve;

    public Animator animator;

    private float initialFov;

    void Start()
    {
        currentBulletCount = magazineSize;
        currentSpreadRange = minSpreadRange;
        if (useCameraRecoil)
        {
            initialFov = playerCamera.fieldOfView;
        }
    }

    public override void Update()
    {
        UpdateConsecutiveShootingValue();
        DecreaseConsecutiveShootingTime();
        UpdateCurrentSpreadRange();
        if (useCameraRecoil)
        {
            UpdateCameraRecoil();
        }
        base.Update();
    }

    private void UpdateCameraRecoil()
    {
        float t = recoilAnimationCurve.Evaluate((Time.time - lastBulletFiredMoment) / recoilTime);
        playerCamera.fieldOfView = Mathf.Lerp(initialFov, initialFov + fovModifier, t);
    }

    private void UpdateConsecutiveShootingValue()
    {
        if (consecutiveShooting && !waitingFireRateCooldown)
        {
            consecutiveShooting = false;
        }
    }

    private void IncreaseConsecutiveShootingTime()
    {
        float bulletFiredMoment = Time.time;
        if (consecutiveShooting)
        {
            float newValue = currentConsecutiveShootingTime + (bulletFiredMoment - lastBulletFiredMoment);
            currentConsecutiveShootingTime = Mathf.Clamp(newValue, 0, timeShootingUntilMaxSpreadRange);
        }
    }

    private void DecreaseConsecutiveShootingTime()
    {
        float timeModifier = Time.deltaTime;
        if (!consecutiveShooting)
        {
            timeModifier *= spreadRangeSlowdownRateWhenNotShooting;
        }
        else
        {
            timeModifier *= spreadRangeSlowdownRateWhenShooting;
        }
        currentConsecutiveShootingTime = Mathf.Max(0, currentConsecutiveShootingTime - timeModifier);
    }

    private void UpdateCurrentSpreadRange()
    {
        float t = spreadRangeGrowthCurve.Evaluate(currentConsecutiveShootingTime / timeShootingUntilMaxSpreadRange);
        currentSpreadRange = Mathf.Lerp(minSpreadRange, maxSpreadRange, t);
    }

    public override bool ShootContinuously()
    {
        if (!enabled)
        {
            return false;
        }

        if (currentBulletCount <= 0)
        {
            Debug.Log(gameObject + "Needs to reload");
            return false;
        }

        if (!waitingFireRateCooldown)
        {
            ShootBullet();
            waitingFireRateCooldown = true;
            IncreaseConsecutiveShootingTime();
            lastBulletFiredMoment = Time.time;
            consecutiveShooting = true;
            SubstractBullet();

            return true;
        }

        return false;
    }

    private void ShootBullet()
    {
        Vector3 startPosition = Vector3.zero;

        // hack
        if (GetComponent<EnemyScript>() != null)
        {
            Quaternion currentRotation = aimingTransform.rotation;
            aimingTransform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
            Quaternion lookAtRotation = aimingTransform.rotation;
            Vector3 newRotation = currentRotation.eulerAngles;
            newRotation.x = lookAtRotation.eulerAngles.x;
            aimingTransform.rotation = Quaternion.Euler(newRotation);
        }

        Vector3 direction = Vector3.Slerp(aimingTransform.forward, Random.onUnitSphere, Mathf.Lerp(currentSpreadRange, 0f, accuracy));

        Debug.Log(gameObject + "Fired bullet from hitscan weapon");
        Debug.DrawRay(aimingTransform.TransformPoint(startPosition), direction.normalized * range, Color.red, 0.5f);

        RaycastHit hit;
        if (Physics.Raycast(aimingTransform.TransformPoint(startPosition), direction, out hit, range, ~ignoreLayerMask))
        {
            HitObject(hit);
        }

        if (animator != null)
        {
            animator.SetTrigger("fired");
        }
        if (weaponAudioSource != null && shootSfx != null)
        {
            weaponAudioSource.clip = shootSfx;
            weaponAudioSource.Play();
        }
    }

    private void HitObject(RaycastHit hit)
    {
        GameObject other = hit.transform.gameObject;
        Debug.Log(gameObject + "Bullet from hitscan weapon hit: " + other);
        if (bulletContactPointPrefab != null)
        {
            if (other.isStatic)
            {
                GameObject instance = Instantiate(bulletContactPointPrefab, hit.point, Quaternion.Euler(0, 0, 0));
                instance.transform.parent = other.transform;
            }
        }
        DamageableEntity damageableEntity;
        if (Util.IsObjectInLayerMask(targetLayerMask, other) &&
            (damageableEntity = other.GetComponent<DamageableEntity>()) != null)
        {
            Debug.Log(gameObject + "Bullet from hitscan weapon is trying to damage: " + other);
            bool damaged = damageableEntity.OnDamage(gameObject, damage);
            Debug.Log(gameObject + "Result of bullet damage: " + damaged);
        }
    }
}
