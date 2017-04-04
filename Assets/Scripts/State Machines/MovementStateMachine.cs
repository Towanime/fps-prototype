using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class MovementStateMachine : MonoBehaviour {
    
    public PlayerInput playerInput;
    public FirstPersonController firstPersonController;

    private StateMachine<MovementStates> fsm;

    void Default_Update()
    {
        firstPersonController.SetInput(playerInput.direction.x, playerInput.direction.z, playerInput.crouching);
    }

    public StateMachine<MovementStates> StateMachine
    {
        get {
            if (fsm == null)
            {
                fsm = StateMachine<MovementStates>.Initialize(this, MovementStates.Default);
            }
            return fsm;
        }
    }
}
