using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {
	public Transform playerPos;
	public Animator myAnimator;
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
	private NavMeshAgent myNav;
	public HitScanWeapon hitScan;
	public EnemyHealth myHealth;
	public float HidingLife;

	//Charge variables
	private Vector3 LastPlayerPos;
	private Vector3 ChargeDir;
	public float speed;
	private Vector3 walkAroundPoint;
	public float walkAroundCD;
	public float walkAroundRadius;

	//hiding variables
	public GameObject[] hidingPoints;
	private Vector3 hideSpotPos;
	public float HidingRange;
	public float stopHidingRange;

	//Minos var
	public float punchRange;
	public float punchTime;



	void Start () {		
		currentState = "none";
		playerPos = GameObject.FindGameObjectWithTag ("Player").transform;
		myNav = gameObject.GetComponent<NavMeshAgent> ();
		if (EnemyCode == 1) {
			hidingPoints = GameObject.FindGameObjectsWithTag ("hidingSpot");
		}
		//cc = gameObject.GetComponent<CharacterController> ();
	}

	// Update is called once per frame
	void Update () {
		RunStates ();
	}
	void RunStates(){
		playerDistance = Vector3.Distance (playerPos.position, transform.position);
		if(canAttack && EnemyCode == 1 && playerDistance <= HidingRange && currentState != "isGettingCover" && myHealth.currentLife <= HidingLife){
			CheckCover ();
		}
		else if(canAttack && EnemyCode == 2 && playerDistance <= punchRange && currentState == "isAttacking"){
			StartWalkAround ();
		}
		else if (canAttack && currentState == "isIdling") {
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
		case "isWalkingAround":		WalkAround ();		break;
		}
	}
	void ResetStates(){
		StopIdle ();
		StopAttack ();
		StopMove ();
		StopGetHit ();
		StopGetCover ();
		StopRandShoot ();
		StopWalkAround ();
		StoplookPlayer ();
		stateTime = 0f;
		shootTime = 0f;
		myAnimator.SetBool ("Idle", false);
		if (EnemyCode == 1) {
			myAnimator.SetBool ("Shoot", false);
			myAnimator.SetBool ("Walk", false);
		}
		if (EnemyCode == 2) {
			myAnimator.SetBool ("Run", false);
			myAnimator.SetBool ("Punch", false);
		}
	}
	//Idle

	void StartIdle(){
		ResetStates ();
		currentState = "isIdling";
		myAnimator.SetBool ("Idle", true);
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
		if (EnemyCode == 1) {
			myAnimator.SetBool ("Shoot", true);
		}
		if (EnemyCode == 2) {
			myAnimator.SetBool ("Run", true);
		}
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
		myAnimator.SetBool ("Walk", true);
	}
	void GetCover(){
		myNav.SetDestination (hideSpotPos);
		if (Vector3.Distance (transform.position, hideSpotPos) <= stopHidingRange) {
			myNav.ResetPath ();
			StopGetCover ();
		}
	}
	void StopGetCover(){
		currentState = "none";
	}

	//walk around the player
	void StartWalkAround(){
		ResetStates ();
		myAnimator.SetBool ("Punch", true);
		currentState = "isWalkingAround";
		stateTime = 0f;
	}
	void WalkAround(){
		stateTime += Time.deltaTime;
		if (stateTime >= punchTime) {
			myAnimator.SetBool ("Punch", false);
			myAnimator.SetBool ("Idle", true);
			if (stateTime >= walkAroundCD) {
				StopWalkAround ();
				stateTime = 0;
			}
		}
	}
	void StopWalkAround(){
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
		if (hitScan.currentBulletCount > 0) {
			hitScan.ShootContinuously ();
		} else {
			shootTime += Time.deltaTime;
			if (shootTime >= shootCd) {
				hitScan.Reload ();
				shootTime = 0f;
			}
		}
	}

	void Charge(){
		//myNav.SetDestination (LastPlayerPos);
		myNav.Move (ChargeDir * Time.deltaTime * speed);
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
	//Move around player Function
	void CheckCover(){
		Debug.Log ("funciona!");
		float minDistance = 1000;
		float HidingSpotDistance;
		float playerToHideDistance;
		for(int i = 0; i<hidingPoints.Length;i++){
			HidingSpotDistance = Vector3.Distance (transform.position, hidingPoints [i].transform.position);
			playerToHideDistance =  Vector3.Distance (playerPos.position, hidingPoints [i].transform.position);
			if( HidingSpotDistance < minDistance && HidingSpotDistance > stopHidingRange && HidingSpotDistance < playerToHideDistance){
				hideSpotPos = hidingPoints [i].transform.position;
				minDistance = Vector3.Distance (transform.position, hidingPoints [i].transform.position);
			}
		}
		if (minDistance < 1000) {
			bool checkpath;
			NavMeshPath myPath = new NavMeshPath();
			checkpath = myNav.CalculatePath (hideSpotPos, myPath);
			if(myPath.status == NavMeshPathStatus.PathComplete)
				StartGetCover ();
		}
	}
	//Public functions
	public void PlayerLocated(){
		canAttack = true;
	}
	public void PlayerMissing(){
		canAttack = false;
	}
	public void PlayerOnTrigger(){
		if (EnemyCode == 2) {
			StopAttack ();//i need star moving around the player here
			StartWalkAround();

		}
	}
}

