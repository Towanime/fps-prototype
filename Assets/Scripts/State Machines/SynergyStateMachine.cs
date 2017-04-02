using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class SynergyStateMachine : MonoBehaviour {
    
    StateMachine<SynergyStates> fsm;
    public PlayerInput playerInput;
    // other components
    public PlayerMovement playerMovement;
    public CameraController cameraController;
    public Animator animator;
    // this should not be here but in a rush!
    public float attackDuration;
    // ui thingy rush mode
    public GameObject manipulatorUi;
    public GameObject attackUi;
    private float currentDuration;
    private AudioSource audioSource;
    public AudioClip swordSfx;

    void Awake()
    {
        fsm = StateMachine<SynergyStates>.Initialize(this, SynergyStates.Default);
        attackUi.SetActive(true);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
    }

    void Idle_Enter()
    {
        playerMovement.Disabled = false;
    }

    void Idle_Update()
    {
        /*bool inSight = manipulationComponent.InSight();
        if (inSight)
        {
            manipulatorUi.SetActive(true);
            attackUi.SetActive(false);
        }
        else
        {
            manipulatorUi.SetActive(false);
            attackUi.SetActive(true);
        }*/
        /*if (playerInput.disk && disk.CurrentState == DiskStates.Default)
        {
            fsm.ChangeState(PlayerStates.DiskThrow);
        }
        if (playerInput.architectMode)
        {
            fsm.ChangeState(PlayerStates.ArchitectMode);
        }*/
    }

    void Attack_Enter()
    {
        // enable sword trigger?
        playerMovement.Disabled = true;
        currentDuration = 0;
        animator.SetTrigger("Swing A");
        audioSource.clip = swordSfx;
        audioSource.Play();
    }

    void Attack_Update()
    {
        currentDuration += Time.deltaTime;
        if (currentDuration >= attackDuration)
        {
            fsm.ChangeState(SynergyStates.Default);
        }
    }

    void Attack_Exit()
    {
    }
    
    void ManipulationMode_Enter()
    {
        playerMovement.Disabled = true;
    }

    void ManipulationMode_Update()
    {
        /*if (manipulationComponent.IsDone())
        {
            fsm.ChangeState(SynergyStates.Default);
        }*/
    }

    void ManipulationMode_Exit()
    {
        /*playerMovement.Disabled = false;
        manipulationComponent.Disabled = true;
        manipulatorUi.SetActive(false);*/
    }
}
