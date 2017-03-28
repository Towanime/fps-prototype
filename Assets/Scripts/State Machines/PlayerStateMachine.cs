using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class PlayerStateMachine : MonoBehaviour {
    
    StateMachine<PlayerStates> fsm;
    public PlayerInput playerInput;
    // other components
    public PlayerMovement playerMovement;
    public Manipulator manipulationComponent;
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
        fsm = StateMachine<PlayerStates>.Initialize(this, PlayerStates.Idle);
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
        bool inSight = manipulationComponent.InSight();
        if (inSight)
        {
            manipulatorUi.SetActive(true);
            attackUi.SetActive(false);
        }
        else
        {
            manipulatorUi.SetActive(false);
            attackUi.SetActive(true);
        }
        if ((playerInput.attack || playerInput.disk) && inSight)
        {
            fsm.ChangeState(PlayerStates.ManipulationMode);
        }
        if (playerInput.attack && !inSight)
        {
            fsm.ChangeState(PlayerStates.Attack);
        }
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
            fsm.ChangeState(PlayerStates.Idle);
        }
    }

    void Attack_Exit()
    {
    }
    
    void ManipulationMode_Enter()
    {
        playerMovement.Disabled = true;
        manipulationComponent.Disabled = false;
        manipulationComponent.Activate();
    }

    void ManipulationMode_Update()
    {
        if (manipulationComponent.IsDone())
        {
            fsm.ChangeState(PlayerStates.Idle);
        }
    }

    void ManipulationMode_Exit()
    {
        playerMovement.Disabled = false;
        manipulationComponent.Disabled = true;
        manipulatorUi.SetActive(false);
    }
}
