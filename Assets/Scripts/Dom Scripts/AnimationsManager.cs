using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationsManager : MonoBehaviour {

	public bool synergyActive = false;

	public LoadingBar loadingBar;
	public Image outerLinesLeft;
	public Image outerLinesRight;
	public Image outerLinesTopLeft;
	public Image outerLinesTopRight;
	public Image innerLinesLeft;
	public Image innerLinesRight;
	public Image healthShell;
	public Image synergyShell;
	public Image ammoCircle;

	void Start () {
		
		outerLinesLeft.GetComponent<Animator>().speed = 0;
		outerLinesRight.GetComponent<Animator>().speed = 0;
		outerLinesTopLeft.GetComponent<Animator>().speed = 0;
		outerLinesTopRight.GetComponent<Animator>().speed = 0;
		innerLinesLeft.GetComponent<Animator>().speed = 0;
		innerLinesRight.GetComponent<Animator>().speed = 0;
		healthShell.GetComponent<Animator>().speed = 0;
		synergyShell.GetComponent<Animator>().speed = 0;
		ammoCircle.GetComponent<Animator>().speed = 0;
	}

	void Update () {

		if (synergyActive == false && ammoCircle.GetComponent<Animator> ().enabled == false) {
			outerLinesLeft.GetComponent<Animator> ().speed = 1;
			outerLinesRight.GetComponent<Animator> ().speed = 1;
			outerLinesTopLeft.GetComponent<Animator> ().speed = 1;
			outerLinesTopRight.GetComponent<Animator> ().speed = 1;
			innerLinesLeft.GetComponent<Animator> ().speed = 1;
			innerLinesRight.GetComponent<Animator> ().speed = 1;
			healthShell.GetComponent<Animator> ().speed = 1;
			synergyShell.GetComponent<Animator> ().speed = 1;
			ammoCircle.GetComponent<Animator> ().speed = 1;
		}

		//Bottom Left
		if (loadingBar.loadingBarDimensions.x >= 800f && outerLinesLeft.GetComponent<Animator>().speed == 0){
			
			outerLinesLeft.GetComponent<Animator>().speed = 1;
		}
		if (loadingBar.loadingBarDimensions.x <= 1420f && outerLinesLeft.GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).IsName ("OuterLinesLeftIdle")) {

			outerLinesLeft.GetComponent<Animator> ().speed = 0;
			outerLinesLeft.GetComponent<Animator> ().Play ("OuterLinesLeftIdle", 0, 0);
		} else if (loadingBar.loadingBarDimensions.x >= 1420f){
			
			outerLinesLeft.GetComponent<Animator> ().speed = 1;
		}
		if (synergyActive == false && outerLinesLeft.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("OuterLinesLeftSyn")) {

			outerLinesLeft.GetComponent<Animator> ().speed = 0;
			outerLinesLeft.GetComponent<Animator> ().Play ("OuterLinesLeftSyn", 0, 0);
		}
		//Bottom Right
		if (loadingBar.loadingBarDimensions.x >= 800f && outerLinesRight.GetComponent<Animator>().speed == 0){

			outerLinesRight.GetComponent<Animator>().speed = 1;
		}
		if (loadingBar.loadingBarDimensions.x <= 1420f && outerLinesRight.GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).IsName ("OuterLinesRightIdle")) {

			outerLinesRight.GetComponent<Animator> ().speed = 0;
			outerLinesRight.GetComponent<Animator> ().Play ("OuterLinesRightIdle", 0, 0);
		} else if (loadingBar.loadingBarDimensions.x >= 1420f){

			outerLinesRight.GetComponent<Animator> ().speed = 1;
		}
		if (synergyActive == false && outerLinesRight.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("OuterLinesRightSyn")) {

			outerLinesRight.GetComponent<Animator> ().speed = 0;
			outerLinesRight.GetComponent<Animator> ().Play ("OuterLinesRightSyn", 0, 0);
		}
		//Top Left
		if (loadingBar.loadingBarDimensions.x >= 1000f && outerLinesTopLeft.GetComponent<Animator>().speed == 0){

			outerLinesTopLeft.GetComponent<Animator>().speed = 1;
		}
		if (loadingBar.loadingBarDimensions.x <= 1420f && outerLinesTopLeft.GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).IsName ("OuterLinesTopLeftIdle")) {

			outerLinesTopLeft.GetComponent<Animator> ().speed = 0;
			outerLinesTopLeft.GetComponent<Animator> ().Play ("OuterLinesTopLeftIdle", 0, 0);
		} else if (loadingBar.loadingBarDimensions.x >= 1420f){

			outerLinesTopLeft.GetComponent<Animator> ().speed = 1;
		}
		if (synergyActive == false && outerLinesTopLeft.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("OuterLinesTopLeftSyn")) {

			outerLinesTopLeft.GetComponent<Animator> ().speed = 0;
			outerLinesTopLeft.GetComponent<Animator> ().Play ("OuterLinesTopLeftSyn", 0, 0);
		}
		//Top Right
		if (loadingBar.loadingBarDimensions.x >= 1000f && outerLinesTopRight.GetComponent<Animator>().speed == 0){

			outerLinesTopRight.GetComponent<Animator>().speed = 1;
		}
		if (loadingBar.loadingBarDimensions.x <= 1420f && outerLinesTopRight.GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).IsName ("OuterLinesTopRightIdle")) {

			outerLinesTopRight.GetComponent<Animator> ().speed = 0;
			outerLinesTopRight.GetComponent<Animator> ().Play ("OuterLinesTopRightIdle", 0, 0);
		} else if (loadingBar.loadingBarDimensions.x >= 1420f){

			outerLinesTopRight.GetComponent<Animator> ().speed = 1;
		}
		if (synergyActive == false && outerLinesTopRight.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("OuterLinesTopRightSyn")) {

			outerLinesTopRight.GetComponent<Animator> ().speed = 0;
			outerLinesTopRight.GetComponent<Animator> ().Play ("OuterLinesTopRightSyn", 0, 0);
		}
		//Inner Left
		if (loadingBar.loadingBarDimensions.x >= 1200f && innerLinesLeft.GetComponent<Animator>().speed == 0){

			innerLinesLeft.GetComponent<Animator>().speed = 1;
		}
		if (loadingBar.loadingBarDimensions.x <= 1420f && innerLinesLeft.GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).IsName ("InnerLinesLeftIdle")) {

			innerLinesLeft.GetComponent<Animator> ().speed = 0;
			innerLinesLeft.GetComponent<Animator> ().Play ("InnerLinesLeftIdle", 0, 0);
		} else if (loadingBar.loadingBarDimensions.x >= 1420f){

			innerLinesLeft.GetComponent<Animator> ().speed = 1;
		}
		if (synergyActive == false && innerLinesLeft.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("InnerLinesLeftSyn")) {

			innerLinesLeft.GetComponent<Animator> ().speed = 0;
			innerLinesLeft.GetComponent<Animator> ().Play ("InnerLinesLeftSyn", 0, 0);
		}
		//Inner Right
		if (loadingBar.loadingBarDimensions.x >= 1200f && innerLinesRight.GetComponent<Animator>().speed == 0){

			innerLinesRight.GetComponent<Animator>().speed = 1;
		}
		if (loadingBar.loadingBarDimensions.x <= 1420f && innerLinesRight.GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).IsName ("InnerLinesRightIdle")) {

			innerLinesRight.GetComponent<Animator> ().speed = 0;
			innerLinesRight.GetComponent<Animator> ().Play ("InnerLinesRightIdle", 0, 0);
		} else if (loadingBar.loadingBarDimensions.x >= 1420f){

			innerLinesRight.GetComponent<Animator> ().speed = 1;
		}
		if (synergyActive == false && innerLinesRight.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("InnerLinesRightSyn")) {

			innerLinesRight.GetComponent<Animator> ().speed = 0;
			innerLinesRight.GetComponent<Animator> ().Play ("InnerLinesRightSyn", 0, 0);
		}
		//Health
		if (loadingBar.loadingBarDimensions.x >= 1400f && healthShell.GetComponent<Animator>().speed == 0){

			healthShell.GetComponent<Animator>().speed = 1;
		}
	}
}
