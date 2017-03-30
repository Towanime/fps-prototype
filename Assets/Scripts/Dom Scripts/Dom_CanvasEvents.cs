using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dom_CanvasEvents : MonoBehaviour {

	public Dom_Typewriter typeWriter;
	public float delay = 1f;
	// Use this for initialization
	void Start () {
		StartCoroutine(BootingDialog ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator BootingDialog() {
	
		yield return new WaitForSeconds (delay);
		typeWriter.fullText = "NEW HARDWARE DETECTED";
		typeWriter.StartCoroutine ("ShowText");

		yield return new WaitForSeconds (delay);
		typeWriter.fullText = "FIRMWARE UPDATED";
		typeWriter.StartCoroutine ("ShowText");

		yield return new WaitForSeconds (delay);
		typeWriter.GetComponent<Text> ().color = Color.red;
		typeWriter.fullText = "EMERGENCY PROTOCOL ACTIVATED";
		typeWriter.delay = 0.12f;
		typeWriter.StartCoroutine ("ShowText");

		delay = 6f;
		yield return new WaitForSeconds (delay);
		typeWriter.GetComponent<Text> ().color = Color.white;
		typeWriter.fullText = "BOOTING...";
		typeWriter.GetComponent<Text> ().text = typeWriter.fullText;
	}
}
