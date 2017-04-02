using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class PlayerStateMachine : MonoBehaviour {
    
    StateMachine<PlayerStates> fsm;
    public PlayerInput playerInput;

    public Inventory inventory;

    void Awake()
    {
        fsm = StateMachine<PlayerStates>.Initialize(this, PlayerStates.Idle);
    }

    void Update()
    {
    }

    void Idle_Enter()
    {
        
    }

    void Idle_Update()
    {
        if (playerInput.shot)
        {
            inventory.GetCurrentWeapon().ShootOnce();
        } else if (playerInput.shooting)
        {
            inventory.GetCurrentWeapon().ShootContinuously();
        }
    }

    void Idle_Exit()
    {

    }

    void Shooting_Update()
    {
        
    }
}
