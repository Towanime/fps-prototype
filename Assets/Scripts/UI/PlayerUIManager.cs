using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour {

    public MeterBar healthMeterBar;
    public PlayerHealth playerHealth;

    public MeterBar synergyMeterBar;
    public Synergy synergy;

    public MeterCircle ammoMeterBar;
    public Inventory inventory;

    void Start()
    {
        ammoMeterBar.SetMeterMaxAmount(inventory.GetCurrentWeapon().magazineSize);
    }

    void Update ()
    {
        healthMeterBar.meterAmount = (int)Mathf.Floor(playerHealth.currentLife);
        synergyMeterBar.meterAmount = (int)Mathf.Floor(synergy.currentAmount);
        ammoMeterBar.meterAmount = inventory.GetCurrentWeapon().currentBulletCount;
    }
}
