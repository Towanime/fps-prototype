using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasEvents : MonoBehaviour {

	//cutscene
	public Typewriter typeWriter;
	public LoadingBar loadingBar;
	public Text bootText;
	public Image blackBG;
	public Image dialogBox;

	public Camera fpsCharacterCam;

	public float delay = 1f;
	public float positionTweak = -20f;

	void Start () {
		blackBG.GetComponent<Animator> ().enabled = false;
		fpsCharacterCam.GetComponent<Animator> ().enabled = false;
		dialogBox.GetComponent<Animator> ().enabled = false;
		bootText.GetComponent<Animator> ().enabled = false;

		StartCoroutine(BootingDialog ());
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

		bootText.rectTransform.position -= new Vector3 (0, positionTweak, 0);

		typeWriter.fullText = "BOOTING...";
		typeWriter.GetComponent<Text> ().text = typeWriter.fullText;
		loadingBar.isBooting = true;

		delay = 3f;
		yield return new WaitForSeconds (delay);
		blackBG.GetComponent<Animator> ().enabled = true;
		delay = 6f;
		yield return new WaitForSeconds (delay);
		blackBG.GetComponent<Animator> ().enabled = false;

		fpsCharacterCam.GetComponent<Animator> ().enabled = true;
		delay = 5f;
		yield return new WaitForSeconds (delay);
		fpsCharacterCam.GetComponent<Animator> ().enabled = false;

		bootText.rectTransform.position += new Vector3 (0, positionTweak, 0);
		typeWriter.fullText = "WELCOME";
		typeWriter.delay = 0.05f;
		typeWriter.StartCoroutine ("ShowText");
		delay = 1f;
		yield return new WaitForSeconds (delay);
		dialogBox.GetComponent<Animator> ().enabled = true;
		bootText.GetComponent<Animator> ().enabled = true;
		delay = 3f;
		yield return new WaitForSeconds (delay);
		dialogBox.GetComponent<Animator> ().enabled = false;
		bootText.GetComponent<Animator> ().enabled = false;

	}
}
