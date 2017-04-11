using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPickup : MonoBehaviour {

	public HitScanWeapon hitScanWeapon;
	public GameObject player;
	public GameObject weapon;
	public CadController cadController;
	public float delay;

	public AudioSource pickup;
	public AudioClip pickupReload;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider) {
		
		weapon.SetActive (true);
		hitScanWeapon.enabled = true;
		cadController.positionState = 3;
		StartCoroutine(ReloadSound ());

	}

	IEnumerator ReloadSound() {
		pickup.PlayOneShot (pickupReload);
		delay = 1f;
		yield return new WaitForSeconds (delay);
		Destroy (gameObject);
	}
}
