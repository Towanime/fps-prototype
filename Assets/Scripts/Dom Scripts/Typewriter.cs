using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typewriter : MonoBehaviour {

	public float delay = 0.1f;
	public string fullText;
	public string currentText = "";

	IEnumerator ShowText(){
		for (int i = 0; i < fullText.Length+1; i++) {
			currentText = fullText.Substring (0,i);
			this.GetComponent<Text> ().text = currentText;
			yield return new WaitForSeconds (delay);
		}
	}
}
