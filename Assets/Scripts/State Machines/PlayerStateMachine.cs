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
    public AnimationCurve dashingSpeedCurve;
    public GameObject player;
    public float dashingDuration;
    public float dashingMaxSpeed;
    public int dashCost;
    public AnimationsManager animationsManager;
    public Crosshair crosshair;
    public Collider playerCollider;
    public PlayerStates startingState = PlayerStates.Booting;

    private StateMachine<PlayerStates> fsm;
    private StateMachine<MovementStates> movementStateMachine;
    private float stateEnterTime;
    private Vector3 dashDirection;
    private CharacterController characterController;

    void Awake()
    {
        movementStateMachine = GetComponent<MovementStateMachine>().StateMachine;
        fsm = StateMachine<PlayerStates>.Initialize(this, startingState);
        characterController = player.GetComponent<CharacterController>();
    }

    void Booting_Enter()
    {
        movementStateMachine.ChangeState(MovementStates.InputDisabled);
        crosshair.enabled = false;
        playerCollider.enabled = false;
    }

    void Booting_Update()
    {
        if (animationsManager.introFinished)
        {
            fsm.ChangeState(PlayerStates.Default);
        }
    }

    void Default_Enter()
    {
        movementStateMachine.ChangeState(MovementStates.Default);
        crosshair.enabled = true;
        playerCollider.enabled = true;
    }

    public void ActivateSynergy()
    {
        synergy.active = true;
        animationsManager.synergyBarSpawn = true;
    }

    void Default_Update()
    {
        UpdateSynergyInput();
        if (inventory.GetCurrentWeapon().CurrentBulletCount <= 0 || (playerInput.reloaded && !inventory.GetCurrentWeapon().FullyLoaded))
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
        } else if (playerInput.dashed)
        {
            if (synergy.Consume(dashCost))
            {
                fsm.ChangeState(PlayerStates.Dashing);
            }
        }
    }

    private void UpdateSynergyInput()
    {
        if (synergy.active)
        {
            synergy.CurrentState = playerInput.synergy ? Synergy.SynergyState.DEPLETING : Synergy.SynergyState.RECOVERING;
            animationsManager.synergyActive = synergy.CurrentState == Synergy.SynergyState.DEPLETING;
        }
    }

    void Reloading_Enter()
    {
        // start reloading animation
        weaponAnimator.SetBool("reloading", true);
    }

    void Reloading_Update()
    {
        UpdateSynergyInput();
        Debug.Log("Player reloading");
        // interrupt reloading if the player performs an action like switch weapon
        if (playerInput.threwShield)
        {
            bool thrown = stationaryShieldPower.ThrowShield(synergy);
            if (thrown)
            {
                fsm.ChangeState(PlayerStates.Default);
            }
        } else if (playerInput.dashed)
        {
            if (synergy.Consume(dashCost))
            {
                fsm.ChangeState(PlayerStates.Dashing);
            }
        }
        AnimatorStateInfo animatorStateInfo = weaponAnimator.GetCurrentAnimatorStateInfo(0);
        if (animatorStateInfo.IsName("Reloading") && animatorStateInfo.normalizedTime >= 1)
        {
            OnReloadingAnimationFinished();
        }
    }

    void Dashing_Enter()
    {
        synergy.CurrentState = Synergy.SynergyState.DEPLETING;
        stateEnterTime = Time.time;
        movementStateMachine.ChangeState(MovementStates.InputDisabled);
        // Calculate dash direction
        Quaternion inputAngle = Quaternion.LookRotation(playerInput.direction);
        dashDirection = inputAngle * player.transform.forward;
        dashDirection = dashDirection.normalized;
    }

    void Dashing_Update()
    {
        float t = (Time.time - stateEnterTime) / dashingDuration;
        float speedDelta = dashingSpeedCurve.Evaluate((Time.time - stateEnterTime) / dashingDuration);
        float currentSpeed = Mathf.Lerp(0, dashingMaxSpeed, speedDelta);

        Vector3 distanceToMove = dashDirection * currentSpeed * Time.deltaTime;
        Vector3 nextPosition = distanceToMove + player.transform.position;
        characterController.Move(distanceToMove);

        if (t >= 1)
        {
            fsm.ChangeState(PlayerStates.Default);
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

    public PlayerStates GetCurrentState()
    {
        return fsm.State;
    }
}
