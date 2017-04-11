using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCharge : MonoBehaviour
{
    public Animator animator;
    public Collider doorCollider;
    public PlayerStateMachine playerStateMachine;
    private bool triggered;

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckCharge();
        }
    }

    public void OnTriggerExit(Collider other)
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckCharge();
        }
    }

    public void CheckCharge()
    {
        if(!triggered && playerStateMachine.GetCurrentState() == PlayerStates.Dashing)
        {
            animator.SetTrigger("Destroy");
            triggered = true;
            doorCollider.enabled = false;
        }
    }
}
