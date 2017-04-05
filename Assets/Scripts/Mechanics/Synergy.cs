using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synergy : MonoBehaviour {

    public float maxAmount;
    public float currentAmount;
    public float recoverRate;
    public float depleteRate;
    public bool recovering;
    public bool depleting;

    void Update()
    {
        if (recovering)
        {
            Recover(recoverRate * Time.deltaTime);
        }
        if (depleting)
        {
            Consume(depleteRate * Time.deltaTime);
        }
    }

    public bool Consume(float amount)
    {
        if (!enabled)
        {
            return false;
        }
        float absAmount = Mathf.Abs(amount);
        if (currentAmount < absAmount)
        {
            return false;
        }
        currentAmount -= absAmount;
        return false;
    }

    public void Recover(float amount)
    {
        if (!enabled)
        {
            return;
        }
        float absAmount = Mathf.Abs(amount);
        currentAmount = Mathf.Min(maxAmount, currentAmount + absAmount);
    }
}
