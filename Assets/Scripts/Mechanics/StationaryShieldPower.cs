using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryShieldPower : MonoBehaviour {

    [Tooltip("Origin of the bullet, its forward is used as the aiming direction.")]
    public Transform aimingTransform;
    public GameObject projectilePrefab;
    public float shootForce = 750;
    public int synergyCost = 10;
    public float cooldownTime;
    private float lastThrowTime;
    private GameObject deployedShield;

    public bool ThrowShield(Synergy synergy)
    {
        bool cooldownFinished = Time.time - lastThrowTime >= cooldownTime;
        if (cooldownFinished && synergy.Consume(synergyCost))
        {
            GameObject projectile = Instantiate(projectilePrefab, aimingTransform.position, Quaternion.Euler(0, 0, 0));
            projectile.GetComponent<StationaryShieldProjectile>().stationaryShieldPower = this;
            projectile.GetComponent<Rigidbody>().AddForce(aimingTransform.forward.normalized * shootForce);
            lastThrowTime = Time.time;
            return true;
        }
        return false;
    }

    public void OnShieldDeploy(GameObject deployedShield)
    {
        Destroy(this.deployedShield);
        this.deployedShield = deployedShield;
    }
}
