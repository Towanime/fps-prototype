using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Tooltip("Delay between bullets.")]
    public float fireRate = 0.05f;
    [Tooltip("Bullet damage.")]
    public float damage = 1;
    [Tooltip("How many bullets can be shot before reloading.")]
    public int magazineSize = 15;
    [Tooltip("How many bullets are substracted after shooting once.")]
    public int bulletsPerShot = 1;

    protected float lastBulletFiredMoment;
    protected bool waitingFireRateCooldown;
    protected int currentBulletCount;

    void Update()
    {
        if (waitingFireRateCooldown)
        {
            if (Time.time - lastBulletFiredMoment >= fireRate)
            {
                waitingFireRateCooldown = false;
            }
        }
    }

    public virtual bool ShootOnce()
    {
        return false;
	}

    public virtual bool ShootContinuously()
    {
        return false;
    }

    public virtual void SubstractBullet()
    {
        currentBulletCount -= bulletsPerShot;
    }
}
