﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	public Transform playerPos;
	public float playerDistance;
	public float Range;
	public Quaternion rotation;
	public float rotationSpeed;
	public string currentState;
	public GameObject EnemyModel;
	public Transform BulletExit;
	public bool canAttack;
	public float startShootCd;
	public float shootCd;
	public float stopShootCd;
	private float stateTime;
	private float shootTime;
	public float EnemyCode;
	public Vector3 LastPlayerPos;
	public float speed;
	private CharacterController cc;
	public Vector3 ChargeDir;

	void Start () {		
		currentState = "none";
		playerPos = GameObject.FindGameObjectWithTag ("Player").transform;
		cc = gameObject.GetComponent<CharacterController> ();
	}

	// Update is called once per frame
	void Update () {
		RunStates ();
	}
	void RunStates(){
		if (canAttack && currentState == "isIdling") {
			StartlookPlayer ();
		} else if (!canAttack && (currentState == "isAttacking" || currentState == "isLooking") && EnemyCode != 2) {
			StartRandShoot ();
			}
			else if (currentState == "none")
				StartIdle();
		//Run ongoing states
		switch (currentState) {
		case "isShootingRand":  	RandShoot ();		break;
		case "isGettingHit":  		GetHit (); 			break;
		case "isAttacking":  		Attack (); 			break;
		case "isLooking":  			lookPlayer (); 		break;
		case "isMoving":  			Move (); 			break;
		case "isIdling":  			Idle (); 			break;
		case "isGettingCover":		GetCover ();		break;
		}
	}
	void ResetStates(){
		StopIdle ();
		StopAttack ();
		StopMove ();
		StopGetHit ();
		StopGetCover ();
		stateTime = 0f;
		shootTime = 0f;
	}
	//Idle

	void StartIdle(){
		ResetStates ();
		currentState = "isIdling";
	}
	void Idle(){

	}
	void StopIdle(){
		currentState = "none";
	}

	//Walk

	void StartMove(){
		ResetStates ();
		currentState = "isMoving";
	}
	void Move(){

	}
	void StopMove(){
		currentState = "none";
	}

	//look player
	void StartlookPlayer(){
		ResetStates ();
		currentState = "isLooking";
	}
	void lookPlayer(){
		LookAtPlayer ();
		stateTime += Time.deltaTime;
		if (stateTime >= startShootCd) {
			if (EnemyCode == 2) {
				LastPlayerPos = playerPos.position;
				ChargeDir = LastPlayerPos - transform.position;
			}
			StartAttack ();
		}
	}
	void StoplookPlayer(){
		currentState = "none";
	}
	//Attack

	void StartAttack(){
		ResetStates ();
		currentState = "isAttacking";
	}
	void Attack(){
		if (EnemyCode == 1) {
			LookAtPlayer ();
			Shoot ();
		}
		if(EnemyCode == 2)
			Charge ();
		if (EnemyCode == 3) {
			LookAtPlayer ();
			ShootBall ();
		}
	}
	void StopAttack(){
		currentState = "none";
	}
	//Random Attack when missing Player
	void StartRandShoot(){
		ResetStates ();
		currentState = "isShootingRand";
	}
	void RandShoot(){
		stateTime += Time.deltaTime;
		Shoot ();
		if (stateTime >= stopShootCd) {
			StopRandShoot();
		} 
	}
	void StopRandShoot(){
		currentState = "none";
	}

	//GetHit

	void StartGetHit(){
		ResetStates ();
		currentState = "isGettingHit";
	}
	void GetHit(){

	}
	void StopGetHit(){
		currentState = "none";
	}
	//Get Cover

	void StartGetCover(){
		ResetStates ();
		currentState = "isGettingCover";
	}
	void GetCover(){

	}
	void StopGetCover(){
		currentState = "none";
	}
	//Private functions
	void TurnToPlayer(){
		Vector3 Direction = playerPos.position - transform.position;
		rotation = Quaternion.LookRotation (Direction);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotationSpeed);
	}

	//Shoot function
	void Shoot(){
		shootTime += Time.deltaTime;
		if(shootTime >= shootCd){
			//cast shoot and animation
			shootTime = 0f;
		}
	}

	void Charge(){
		cc.Move (ChargeDir * Time.deltaTime * speed);
		shootTime += Time.deltaTime;
		if(shootTime >= shootCd){
			ChargeDir = Vector3.zero;
			StopAttack ();
		}
	}


	void ShootBall(){
		shootTime += Time.deltaTime;
		if(shootTime >= shootCd){
			//cast shoot and animation
			shootTime = 0f;
		}
	}
	//stare the player
	void LookAtPlayer(){
			Vector3 Direction = playerPos.position - transform.position;
			rotation = Quaternion.LookRotation (Direction);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotationSpeed);
	}
	//Public functions
	public void PlayerLocated(){
			canAttack = true;
	}
	public void PlayerMissing(){
			canAttack = false;
	}
	public void PlayerOnTrigger(){
		StopAttack ();
	}
}
