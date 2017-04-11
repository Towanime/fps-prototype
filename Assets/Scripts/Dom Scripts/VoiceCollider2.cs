using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceCollider2 : MonoBehaviour {

	public CadController cadController;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider collider) {
		cadController.positionState = 13;
		Destroy (gameObject);
	}
}
