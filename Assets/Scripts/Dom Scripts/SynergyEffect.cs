using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyEffect : MonoBehaviour {

	public AnimationsManager animationManager;

	public void SynergyEffectTrigger() {
		
		if (animationManager.synergyActive == true) {
			animationManager.outerLinesLeft.GetComponent<Animator> ().speed = 0f;
			animationManager.outerLinesRight.GetComponent<Animator> ().speed = 0f;
			animationManager.outerLinesTopLeft.GetComponent<Animator> ().speed = 0f;
			animationManager.outerLinesTopRight.GetComponent<Animator> ().speed = 0f;
			animationManager.innerLinesLeft.GetComponent<Animator> ().speed = 0f;
			animationManager.innerLinesRight.GetComponent<Animator> ().speed = 0f;
			animationManager.healthShell.GetComponent<Animator> ().speed = 0f;
			animationManager.synergyShell.GetComponent<Animator> ().speed = 0f;
			animationManager.ammoCircle.GetComponent<Animator> ().speed = 0f;
		}
	}
}
