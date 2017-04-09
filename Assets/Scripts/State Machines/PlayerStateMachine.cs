using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class PlayerStateMachine : MonoBehaviour {
    
    public PlayerInput playerInput;
    public Inventory inventory;
    public StationaryShieldPower stationaryShieldPower;
    public Synergy synergy;
    public Animator weaponAnimator;

    private StateMachine<PlayerStates> fsm;
    private StateMachine<MovementStates> movementStateMachine;
    private Coroutine reloadingCoroutine;

    void Awake()
    {
        movementStateMachine = GetComponent<MovementStateMachine>().StateMachine;
        fsm = StateMachine<PlayerStates>.Initialize(this, PlayerStates.Default);
    }

    void Default_Enter()
    {
        movementStateMachine.ChangeState(MovementStates.Default);
    }

    void Default_Update()
    {
        if (inventory.GetCurrentWeapon().CurrentBulletCount <= 0)
        {
            fsm.ChangeState(PlayerStates.Reloading);
        } else if (playerInput.shot)
        {
            inventory.GetCurrentWeapon().ShootOnce();
        } else if (playerInput.shooting)
        {
            inventory.GetCurrentWeapon().ShootContinuously();
        }
        if (playerInput.threwShield)
        {
            stationaryShieldPower.ThrowShield(synergy);
        }
    }

    void Reloading_Enter()
    {
        // start reloading animation
        weaponAnimator.SetBool("reloading", true);
    }

    void Reloading_Update()
    {
        Debug.Log("Player reloading");
        // interrupt reloading if the player performs an action like switch weapon
        if (playerInput.threwShield)
        {
            stationaryShieldPower.ThrowShield(synergy);
            movementStateMachine.ChangeState(MovementStates.Default);
        }
        AnimatorStateInfo animatorStateInfo = weaponAnimator.GetCurrentAnimatorStateInfo(0);
        if (animatorStateInfo.IsName("Reloading") && animatorStateInfo.normalizedTime >= 1)
        {
            OnReloadingAnimationFinished();
        }
    }

    void OnReloadingAnimationFinished()
    {
        Debug.Log("Player reloading finished");
        inventory.GetCurrentWeapon().Reload();
        fsm.ChangeState(PlayerStates.Default);
    }

    void Reloading_Exit()
    {
        weaponAnimator.SetBool("reloading", false);
    }
}
