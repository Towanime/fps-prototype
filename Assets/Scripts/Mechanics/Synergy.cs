using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synergy : MonoBehaviour {

    public bool active;
    public float maxAmount = 100;
    public float recoverRate = 10;
    public float depleteRate = 10;
    public float overheatRecoveryRate = 5;
    private bool overheated;
    private SynergyState currentState = SynergyState.RECOVERING;
    public float currentAmount;

    public enum SynergyState
    {
        RECOVERING,
        DEPLETING
    }

    void Start()
    {
        currentAmount = maxAmount;
    }

    void Update()
    {
        if (currentState == SynergyState.RECOVERING)
        {
            float currentRecoveryRate = overheated ? overheatRecoveryRate : recoverRate;
            Recover(currentRecoveryRate * Time.deltaTime);
            if (currentAmount == maxAmount)
            {
                Overheated = false;
            }
        } else if (currentState == SynergyState.DEPLETING)
        {
            bool consumed = Consume(depleteRate * Time.deltaTime);
            if (!consumed)
            {
                currentAmount = 0;
                Overheated = true;
            }
        }
    }

    public bool Consume(float amount)
    {
        if (!active || overheated)
        {
            return false;
        }
        float absAmount = Mathf.Abs(amount);
        if (currentAmount < absAmount)
        {
            return false;
        }
        currentAmount -= absAmount;
        if (currentAmount == 0)
        {
            Overheated = true;
        }
        return true;
    }

    public void Recover(float amount)
    {
        float absAmount = Mathf.Abs(amount);
        currentAmount = Mathf.Min(maxAmount, currentAmount + absAmount);
    }

    public void RecoverAll()
    {
        currentAmount = maxAmount;
    }

    public bool ConsumeAll()
    {
        if (!active || overheated)
        {
            return false;
        }
        currentAmount = 0;
        Overheated = true;
        return true;
    }

    public bool Overheated
    {
        get { return overheated; }
        set {
            overheated = value;
            if (overheated)
            {
                CurrentState = SynergyState.RECOVERING;
            }
        }
    }

    public SynergyState CurrentState
    {
        get { return currentState; }
        set {
            if (value == SynergyState.DEPLETING && overheated)
            {
                currentState = SynergyState.RECOVERING;
            } else
            {
                currentState = value;
            }
        }
    }
}
