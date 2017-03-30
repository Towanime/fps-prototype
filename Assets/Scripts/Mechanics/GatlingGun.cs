using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GatlingGun : MonoBehaviour {

    public LayerMask targetLayerMask;
    public LayerMask ignoreLayerMask;

    [Tooltip("Offset in coordinates used to randomize the spawn point of the bullet.")]
    public float bulletSpawnPositionRandomOffset = 0.15f;
    [Tooltip("Delay between bullets.")]
    public float fireRate = 0.05f;
    [Tooltip("Optional bullet speed, this will override the speed on the bullet prefab if overrideBulletSpeed is set to true.")]
    public float bulletSpeed = 15f;
    [Tooltip("Multiplier for how fast the overheat should recover when not shooting. Ex: if overheatLimit is 6s, a value of 2 will make it recover in 3s.")]
    public float recoverRate = 2;
    // cooldown vars
    private float currentCooldown;
    private bool wait;
    // did it got overheated?
    private bool isFiringGun = false;
    private bool lastFiringGun = false;
    private Vector2 tmp;

    public Transform camTransform;
    public float range = 400;
    private float offsetFactor = 0;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Fire();
        }
        if (wait)
        {
            currentCooldown += Time.fixedDeltaTime;
            // turn off wait if the time is up
            if (currentCooldown >= fireRate)
            {
                wait = false;
            }
        }
        lastFiringGun = isFiringGun;
        isFiringGun = false;
    }

    public float bulletSpreadSize = 0.3f;
    public float accuracy = 1;

    /// <summary>
    /// Fires a bullet
    /// </summary>
    public bool Fire()
    {
        isFiringGun = true;
        if (!wait)
        {
            Debug.Log("Fired");
            float offset = offsetFactor;
            Vector3 startPosition = new Vector3(Random.Range(-offset, offset), Random.Range(-offset, offset), 1);

            Vector3 direction = Vector3.Slerp(camTransform.forward, Random.onUnitSphere, Mathf.Lerp(bulletSpreadSize, 0f, accuracy));

            Debug.DrawRay(camTransform.TransformPoint(startPosition), direction.normalized * 10, Color.red, 1);

            RaycastHit hit;
            if (Physics.Raycast(camTransform.TransformPoint(startPosition), direction, out hit, range, ~ignoreLayerMask))
            {
                if (Util.IsObjectInLayerMask(targetLayerMask, hit.transform.gameObject))
                {
                    Debug.Log("Hit");
                }
            }


            // start cooldown
            wait = true;
            currentCooldown = 0;
            return true;
        }
        return false;
    }
}
