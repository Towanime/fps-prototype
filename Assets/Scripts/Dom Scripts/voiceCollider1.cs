﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voiceCollider1 : MonoBehaviour {

	public CadController cadController;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider) {
		cadController.positionState = 12;
		Destroy (gameObject);
	}
}
