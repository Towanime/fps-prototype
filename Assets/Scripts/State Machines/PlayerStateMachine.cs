using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class PlayerStateMachine : MonoBehaviour {
    
    public PlayerInput playerInput;
    public Inventory inventory;

    public float reloadingTime = 2f;

    private StateMachine<PlayerStates> fsm;
    private Coroutine reloadingCoroutine;

    void Awake()
    {
        fsm = StateMachine<PlayerStates>.Initialize(this, PlayerStates.Default);
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
    }

    void Reloading_Enter()
    {
        // start reloading animation
        reloadingCoroutine = StartCoroutine(ReloadingCoroutine());
    }

    void Reloading_Update()
    {
        // interrupt reloading if the player performs an action like switch weapon
        Debug.Log("Player reloading");
    }

    void Reloading_Exit()
    {
        StopCoroutine(reloadingCoroutine);
    }

    IEnumerator ReloadingCoroutine()
    {
        yield return new WaitForSeconds(reloadingTime);
        Debug.Log("Player reloading finished");
        inventory.GetCurrentWeapon().Reload();
        fsm.ChangeState(PlayerStates.Default);
    }
}
