using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CadController : MonoBehaviour {

	public int positionState = 0;
	public float speed;
	public float delay;
	private bool audioTrigger;

	public GameObject door1;
	public GameObject door2;

	public Transform targetRotation;
	public Transform targetOne;
	public Transform targetTwo;
	public Transform targetThree;

	public AudioSource cadVoice;
	public AudioClip recording5;
	public AudioClip recording6;
	public AudioClip recording7;
	public AudioClip recording8;
	public AudioClip recording9;

	public Synergy synergyToggle;

	public Component[] cadMesh;
	// Use this for initialization
	void Start () {
		door1.GetComponent<Animator> ().enabled = false;
		door2.GetComponent<Animator> ().enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(targetRotation);

		float step = speed * Time.deltaTime;

		if (positionState == 1) {
			transform.position = Vector3.MoveTowards (transform.position, targetOne.position, step);
			door1.GetComponent<Animator> ().enabled = true;
		}
		if (positionState == 2) {
			transform.position = Vector3.MoveTowards (transform.position, targetTwo.position, step);
		}
		if (positionState == 3 || positionState == 10) {
			transform.position = Vector3.MoveTowards (transform.position, targetThree.position, step);
			door2.GetComponent<Animator> ().enabled = true;
			if (positionState == 3) {
				audioTrigger = true;
			}
		}
		if (positionState == 4) {
			transform.position = Vector3.MoveTowards (transform.position, targetRotation.position, step);
		}

		if (audioTrigger == true) {
			StartCoroutine (MoreDialog ());
			positionState = 10;
			audioTrigger = false;
		}

		if (positionState == 12) {
			cadVoice.PlayOneShot (recording7);
			positionState = 11;
		}
		if (positionState == 13) {
			cadVoice.PlayOneShot (recording8);
			positionState = 11;
		}
		if (positionState == 14) {
			cadVoice.PlayOneShot (recording9);
			positionState = 11;
		}
	}

	IEnumerator MoreDialog() {
		delay = 8f;
		yield return new WaitForSeconds (delay);
		cadVoice.PlayOneShot (recording5);
		delay = 6f;
		yield return new WaitForSeconds (delay);
		positionState = 4;
	}

	void OnTriggerEnter(Collider collider){
		if (positionState == 4) {
			synergyToggle.enabled = true;
			cadMesh = this.gameObject.GetComponentsInChildren<MeshRenderer> ();
			foreach (MeshRenderer joint in cadMesh) {
				joint.enabled = false;
			}
			Destroy(this.gameObject.GetComponentInChildren<ParticleSystem> ());
			this.gameObject.GetComponent<SphereCollider> ().enabled = false;
			cadVoice.PlayOneShot (recording6);
			positionState = 11;
		}
	}
}
