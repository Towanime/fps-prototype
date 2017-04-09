using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synergy : MonoBehaviour {

    public bool active;
    public float maxAmount = 100;
    public float recoverRate;
    public float recoverRateMultiplier = 1;
    public float depleteRate;
    public float depleteRateMultiplier = 1;
    public bool recovering;
    public bool depleting;
    public float currentAmount;

    void Start()
    {
        currentAmount = maxAmount;
    }

    void Update()
    {
        if (recovering)
        {
            Recover(recoverRate * recoverRateMultiplier * Time.deltaTime);
        }
        if (depleting)
        {
            Consume(depleteRate * depleteRateMultiplier * Time.deltaTime);
        }
    }

    public bool Consume(float amount)
    {
        if (!active)
        {
            return false;
        }
        float absAmount = Mathf.Abs(amount);
        if (currentAmount < absAmount)
        {
            return false;
        }
        currentAmount -= absAmount;
        return true;
    }

    public void Recover(float amount)
    {
        if (!active)
        {
            return;
        }
        float absAmount = Mathf.Abs(amount);
        currentAmount = Mathf.Min(maxAmount, currentAmount + absAmount);
    }

    public void RecoverAll()
    {
        currentAmount = maxAmount;
    }

    public bool ConsumeAll()
    {
        if (!active)
        {
            return false;
        }
        currentAmount = 0;
        return true;
    }
}
