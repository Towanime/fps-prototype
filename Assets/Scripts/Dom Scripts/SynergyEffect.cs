using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyEffect : MonoBehaviour {

	public AnimationsManager animationManager;

	public void SynergyEffectTrigger() {
		
		if (animationManager.synergyActive == true) {

			animationManager.introActiveBot = 4;
			animationManager.introActiveTop = 4;
			animationManager.introActiveInTop = 4;
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
	public void SynergyEffectReset() {
		
		animationManager.outerLinesLeft.GetComponent<Animator> ().speed = 0f;
		animationManager.outerLinesLeft.GetComponent<Animator> ().Play ("OuterLinesLeftSyn", 0, 0);

		animationManager.outerLinesRight.GetComponent<Animator> ().speed = 0f;
		animationManager.outerLinesRight.GetComponent<Animator> ().Play ("OuterLinesRightSyn", 0, 0);

		animationManager.outerLinesTopLeft.GetComponent<Animator> ().speed = 0f;
		animationManager.outerLinesTopLeft.GetComponent<Animator> ().Play ("OuterLinesTopLeftSyn", 0, 0);

		animationManager.outerLinesTopRight.GetComponent<Animator> ().speed = 0f;
		animationManager.outerLinesTopRight.GetComponent<Animator> ().Play ("OuterLinesTopRightSyn", 0, 0);

		animationManager.innerLinesLeft.GetComponent<Animator> ().speed = 0f;
		animationManager.innerLinesLeft.GetComponent<Animator> ().Play ("InnerLinesLeftSyn", 0, 0);

		animationManager.innerLinesRight.GetComponent<Animator> ().speed = 0f;
		animationManager.innerLinesRight.GetComponent<Animator> ().Play ("InnerLinesRightSyn", 0, 0);
	}
}
