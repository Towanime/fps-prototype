using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public WeaponType currentWeaponType = WeaponType.HitScan;

    private Dictionary<WeaponType, Weapon> weapons = new Dictionary<WeaponType, Weapon>();

    void Start()
    {
        weapons.Add(WeaponType.HitScan, GetComponent<HitScanWeapon>());
    }

    public enum WeaponType
    {
        HitScan
    }

	public Weapon GetCurrentWeapon()
    {
        return weapons[currentWeaponType];
    }
}
