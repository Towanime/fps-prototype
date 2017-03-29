using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiScript : MonoBehaviour {
	public Transform playerPos;
	public float playerDistance;
	public float Range;
	public Quaternion rotation;
	public float rotationSpeed;
	public string currentState;

	void Start () {		
		currentState = "none";
		playerPos = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	// Update is called once per frame
	void Update () {
		playerDistance = Vector3.Distance (transform.position, playerPos.position);
		if (playerDistance <= Range) {

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
		else if (currentState == "none")
		{StartIdle();}
*/
		//Run ongoing states 8===D
		switch (currentState) {
		case "isGettingHit":  		GetHit (); 		break;
		case "isAttacking":  		Attack (); 		break;
		case "isMoving":  			Move (); 		break;
		case "isIdling":  			Idle (); 		break;
		case "isGettingCover":		GetCover ();	break;
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

	}
	//Walk

	void StartMove(){
		ResetStates ();
		currentState = "isIdling";
	}
	void Move(){

	}
	void StopMove(){

	}
	//Attack

	void StartAttack(){
		ResetStates ();
		currentState = "isIdling";
	}
	void Attack(){
		lookPlayer ();
	}
	void StopAttack(){

	}
	//GetHit

	void StartGetHit(){
		ResetStates ();
		currentState = "isIdling";
	}
	void GetHit(){

	}
	void StopGetHit(){

	}
	//Get Cover

	void StartGetCover(){
		ResetStates ();
		currentState = "isIdling";
	}
	void GetCover(){

	}
	void StopGetCover(){

	}

	void lookPlayer(){
		Vector3 Direction = playerPos.position - transform.position;
		rotation = Quaternion.LookRotation (Direction);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotationSpeed);
	}
}
