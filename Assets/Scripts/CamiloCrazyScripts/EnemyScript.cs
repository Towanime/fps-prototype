using System.Collections;
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
	public float stateTime;

	void Start () {		
		currentState = "none";
		playerPos = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	// Update is called once per frame
	void Update () {
		RunStates ();
		if (canAttack) {
			Vector3 Direction = playerPos.position - transform.position;
			rotation = Quaternion.LookRotation (Direction);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotationSpeed);
		}
	}
	void RunStates(){
		
		/*if (currentState == "isInsideOrb") {
			Orb ();
		}
		else if (timeScared > ScareCooldown && EnemyCode == 1 && currentState == "isGettingFear" && currentState !="isGettingHit" ) {
			startGetScare ();
		}
		else if(currentState == "isIdling" && DogDistance <= attackRange && currentState != "isGettingFear" && vMoving){
			StartAttack ();
		}
		//Walk
		else if ( currentState == "isIdling" && DogDistance > attackRange && vMoving) {
			StartWalk ();
		}
		//Idle
		else 
*/
		if (currentState == "none")
			StartIdle();
		//Run ongoing states 8===D
		switch (currentState) {
		case "isStopRandShoot":  	StopRandShoot ();	break;
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
		stateTime = 0f;
	}
	void lookPlayer(){
		stateTime += Time.deltaTime;
		if (stateTime >= startShootCd) {
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
		stateTime = 0f;
	}
	void Attack(){
		stateTime += Time.deltaTime;
		if (stateTime >= shootCd) {
			//cast shoot and animation
			stateTime = 0f;
		} 
	}
	void StopAttack(){
		currentState = "none";
	}
	//Random Attack when missing Player
	void StartRandShoot(){
		ResetStates ();
		currentState = "isStopRandShoot";
		stateTime = 0f;
	}
	void RandShoot(){
		stateTime += Time.deltaTime;
		if (stateTime >= stopShootCd) {
			//cast shoot and animation
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

	//Public functions
	public void PlayerLocated(){
		if (!canAttack) {
			StartlookPlayer ();
			canAttack = true;
		}
	}
	public void PlayerMissing(){
		if (canAttack) {
			canAttack = false;
			StartRandShoot ();
		}
	}
}

