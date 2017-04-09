using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryShieldProjectile : MonoBehaviour {

    public GameObject stationaryShieldPrefab;
    public StationaryShieldPower stationaryShieldPower;
    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];

        Vector3 direction = transform.position - lastPosition;
        float localY = Quaternion.LookRotation(direction).eulerAngles.y;
        Quaternion localRotation = Quaternion.Euler(0, localY, 0);

        GameObject deployedShield = Instantiate(stationaryShieldPrefab, contactPoint.point, localRotation);
        float worldZRotation = contactPoint.normal.x * -90;
        deployedShield.transform.Rotate(0, 0, worldZRotation, Space.World);

        stationaryShieldPower.OnShieldDeploy(deployedShield);

        Destroy(gameObject);
    }
}
