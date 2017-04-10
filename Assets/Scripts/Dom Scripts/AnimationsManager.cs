using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationsManager : MonoBehaviour {

	public int introActiveBot = 1;
	public int introActiveTop = 1;
	public int introActiveInTop = 1;
	public bool synergyActive = false;
	public bool getHit = false;
	public bool synergyBarSpawn = false;

	public SynergyEffect synergyEffect;
	public MeterCircle meterCircle;

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

		if (synergyActive == false && ammoCircle.GetComponent<Animator> ().enabled == false && introActiveBot < 3) {
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
		//Bottom Lines
		if (loadingBar.loadingBarDimensions.x >= 800f && introActiveBot == 1) {
			
			outerLinesLeft.GetComponent<Animator> ().speed = 1;
			outerLinesRight.GetComponent<Animator> ().speed = 1;
			if (outerLinesLeft.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("OuterLinesLeftIdle")) {
				introActiveBot = 2;
			}
		}

		if (loadingBar.loadingBarDimensions.x <= 1420f && introActiveBot == 2) {

			outerLinesLeft.GetComponent<Animator> ().speed = 0;
			outerLinesLeft.GetComponent<Animator> ().Play ("OuterLinesLeftIdle", 0, 0);
			outerLinesRight.GetComponent<Animator> ().speed = 0;
			outerLinesRight.GetComponent<Animator> ().Play ("OuterLinesRightIdle", 0, 0);
		} else if (loadingBar.loadingBarDimensions.x >= 1420f && synergyActive == false && introActiveBot == 2) {

			outerLinesLeft.GetComponent<Animator> ().speed = 1;
			outerLinesRight.GetComponent<Animator> ().speed = 1;
		}

		if (synergyActive == false && outerLinesLeft.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("OuterLinesLeftSyn") && introActiveBot == 2) {

			outerLinesLeft.GetComponent<Animator> ().speed = 0;
			outerLinesLeft.GetComponent<Animator> ().Play ("OuterLinesLeftSyn", 0, 0);
			outerLinesRight.GetComponent<Animator> ().speed = 0;
			outerLinesRight.GetComponent<Animator> ().Play ("OuterLinesRightSyn", 0, 0);
			introActiveBot = 3;
		}

		if (synergyActive == true && introActiveBot == 3) {

			outerLinesLeft.GetComponent<Animator> ().speed = 1;
			outerLinesRight.GetComponent<Animator> ().speed = 1;
		}

		if (synergyActive == false && introActiveBot == 4) {

			outerLinesLeft.GetComponent<Animator> ().speed = 1;
			outerLinesRight.GetComponent<Animator> ().speed = 1;
			introActiveBot = 3;
		}
		//Top Lines
		if (loadingBar.loadingBarDimensions.x >= 1000f && introActiveTop == 1) {

			outerLinesTopLeft.GetComponent<Animator> ().speed = 1;
			outerLinesTopRight.GetComponent<Animator> ().speed = 1;
			if (outerLinesTopLeft.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("OuterLinesTopLeftIdle")) {
				introActiveTop = 2;
			}
		}

		if (loadingBar.loadingBarDimensions.x <= 1420f && introActiveTop == 2) {

			outerLinesTopLeft.GetComponent<Animator> ().speed = 0;
			outerLinesTopLeft.GetComponent<Animator> ().Play ("OuterLinesTopLeftIdle", 0, 0);
			outerLinesTopRight.GetComponent<Animator> ().speed = 0;
			outerLinesTopRight.GetComponent<Animator> ().Play ("OuterLinesTopRightIdle", 0, 0);
		} else if (loadingBar.loadingBarDimensions.x >= 1420f && synergyActive == false && introActiveTop == 2) {

			outerLinesTopLeft.GetComponent<Animator> ().speed = 1;
			outerLinesTopRight.GetComponent<Animator> ().speed = 1;
		}

		if (synergyActive == false && outerLinesTopLeft.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("OuterLinesTopLeftSyn") && introActiveTop == 2) {

			outerLinesTopLeft.GetComponent<Animator> ().speed = 0;
			outerLinesTopLeft.GetComponent<Animator> ().Play ("OuterLinesTopLeftSyn", 0, 0);
			outerLinesTopRight.GetComponent<Animator> ().speed = 0;
			outerLinesTopRight.GetComponent<Animator> ().Play ("OuterLinesTopRightSyn", 0, 0);
			introActiveTop = 3;
		}

		if (synergyActive == true && introActiveTop == 3) {

			outerLinesTopLeft.GetComponent<Animator> ().speed = 1;
			outerLinesTopRight.GetComponent<Animator> ().speed = 1;
		}

		if (synergyActive == false && introActiveTop == 4) {

			outerLinesTopLeft.GetComponent<Animator> ().speed = 1;
			outerLinesTopRight.GetComponent<Animator> ().speed = 1;
			introActiveTop = 3;
		}
		//Inner Lines
		if (loadingBar.loadingBarDimensions.x >= 1200f && introActiveInTop == 1) {

			innerLinesLeft.GetComponent<Animator> ().speed = 1;
			innerLinesRight.GetComponent<Animator> ().speed = 1;
			if (innerLinesLeft.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("InnerLinesLeftIdle")) {
				introActiveInTop = 2;
			}
		}

		if (loadingBar.loadingBarDimensions.x <= 1420f && introActiveInTop == 2) {

			innerLinesLeft.GetComponent<Animator> ().speed = 0;
			innerLinesLeft.GetComponent<Animator> ().Play ("InnerLinesLeftIdle", 0, 0);
			innerLinesRight.GetComponent<Animator> ().speed = 0;
			innerLinesRight.GetComponent<Animator> ().Play ("InnerLinesRightIdle", 0, 0);
		} else if (loadingBar.loadingBarDimensions.x >= 1420f && synergyActive == false && introActiveInTop == 2) {

			innerLinesLeft.GetComponent<Animator> ().speed = 1;
			innerLinesRight.GetComponent<Animator> ().speed = 1;
		}

		if (synergyActive == false && innerLinesLeft.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("InnerLinesLeftSyn") && introActiveInTop == 2) {

			innerLinesLeft.GetComponent<Animator> ().speed = 0;
			innerLinesLeft.GetComponent<Animator> ().Play ("InnerLinesLeftSyn", 0, 0);
			innerLinesRight.GetComponent<Animator> ().speed = 0;
			innerLinesRight.GetComponent<Animator> ().Play ("InnerLinesRightSyn", 0, 0);
			introActiveInTop = 3;
		}

		if (synergyActive == true && introActiveInTop == 3) {

			innerLinesLeft.GetComponent<Animator> ().speed = 1;
			innerLinesRight.GetComponent<Animator> ().speed = 1;
		}

		if (synergyActive == false && introActiveInTop == 4) {

			innerLinesLeft.GetComponent<Animator> ().speed = 1;
			innerLinesRight.GetComponent<Animator> ().speed = 1;
			introActiveInTop = 3;
		}

		//Health Bar Spawn
		if (loadingBar.loadingBarDimensions.x >= 1420f) {
			
			healthShell.GetComponent<Animator> ().speed = 1;
		}
		//synergy bar spawn
		if (synergyBarSpawn == true) {

			synergyShell.GetComponent<Animator> ().speed = 1;
		}
		//ammo circle spawn
		if (meterCircle.ammoNeedsReload == false && meterCircle.meterAmount != meterCircle.meterMaxAmount) {

			ammoCircle.GetComponent<Animator> ().speed = 1;
		}
		if (meterCircle.ammoNeedsReload == true && meterCircle.meterAmount == meterCircle.meterMaxAmount) {

			meterCircle.ammoNeedsReload = false;
			ammoCircle.GetComponent<Animator> ().speed = 1;
		}
		//hit spaz
		if (getHit == true && introActiveBot != 6) {

			introActiveBot = 5;
			introActiveTop = 5;
			introActiveInTop = 5;
		} 
		if (introActiveTop == 5) {
			outerLinesLeft.GetComponent<Animator> ().Play ("OuterLinesLeftHit", 0, 0);
			outerLinesLeft.GetComponent<Animator> ().speed = 1f;

			outerLinesRight.GetComponent<Animator> ().Play ("OuterLinesRightHit", 0, 0);
			outerLinesRight.GetComponent<Animator> ().speed = 1f;

			outerLinesTopLeft.GetComponent<Animator> ().Play ("OuterLinesTopLeftHit", 0, 0);
			outerLinesTopLeft.GetComponent<Animator> ().speed = 1f;

			outerLinesTopRight.GetComponent<Animator> ().Play ("OuterLinesTopRightHit", 0, 0);
			outerLinesTopRight.GetComponent<Animator> ().speed = 1f;

			innerLinesLeft.GetComponent<Animator> ().Play ("InnerLinesLeftHit", 0, 0);
			innerLinesLeft.GetComponent<Animator> ().speed = 1f;

			innerLinesRight.GetComponent<Animator> ().Play ("InnerLinesRightHit", 0, 0);
			innerLinesRight.GetComponent<Animator> ().speed = 1f;

			introActiveBot = 6;
			introActiveTop = 6;
			introActiveInTop = 6;
		}
		if (getHit == false && introActiveBot == 6) {

			synergyEffect.SynergyEffectReset();
			introActiveBot = 3;
			introActiveTop = 3;
			introActiveInTop = 3;
		}
	}

    public bool HasIntroFinished()
    {
        return loadingBar.loadingBarDimensions.x >= 1420f;
    }
}
