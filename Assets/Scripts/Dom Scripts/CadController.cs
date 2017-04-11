using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CadController : MonoBehaviour {

	public int positionState = 0;
	public float speed;

	public Transform targetRotation;
	public Transform targetOne;
	public Transform targetTwo;
	public Transform targetThree;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(targetRotation);

		float step = speed * Time.deltaTime;

		if (positionState == 1) {
			transform.position = Vector3.MoveTowards (transform.position, targetOne.position, step);
		}
		if (positionState == 2) {
			transform.position = Vector3.MoveTowards (transform.position, targetTwo.position, step);
		}
		if (positionState == 3) {
			transform.position = Vector3.MoveTowards (transform.position, targetThree.position, step);
		}
		if (positionState == 4) {
			transform.position = Vector3.MoveTowards (transform.position, targetRotation.position, step);
		}
	}
}
