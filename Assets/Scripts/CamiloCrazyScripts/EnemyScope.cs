using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScope : MonoBehaviour {

	public EnemyScript myEnemy;
	public float playerDistance;
	public Transform playerPos;
	public float Range;
	public bool playerInRange;
	public GameObject[] playerHitpoints;
	public bool canAttack;
	// Use this for initialization
	void Start(){
		Range = myEnemy.Range;
		playerPos = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHitpoints = GameObject.FindGameObjectsWithTag ("PlayerHitPoint");
	}
	void Update () {
		canAttack = false;
		checkPlayer ();
		if (canAttack) {
			myEnemy.PlayerLocated ();
		} else {
			myEnemy.PlayerMissing ();
		}
	}
	void checkPlayer(){
		playerDistance = Vector3.Distance (transform.position, playerPos.position);
		if (playerDistance <= Range) {
			tryShoot ();
		}
	}

	void tryShoot(){
		RaycastHit hit;//Raycast variable who will get all the raycast info
		for(int i = 0; i < playerHitpoints.Length;i++){
			Vector3 Dir;
			Dir = playerHitpoints [i].transform.position - transform.position; // setting direction betwwen the initial position of the camera and the player
			Physics.Raycast (transform.position, Dir, out hit,playerDistance+3);//doing first raycast detecting objects between the player and the camera
			if (hit.collider.tag == "Player") {
				Debug.DrawRay (transform.position, Dir);
				i = playerHitpoints.Length;
				canAttack = true;
			} else {
				Debug.Log (hit.collider.tag);
			}

		}

	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			myEnemy.PlayerOnTrigger ();
		}
	}
}
