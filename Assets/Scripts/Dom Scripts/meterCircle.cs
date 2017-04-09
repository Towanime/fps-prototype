
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterCircle : MonoBehaviour {

	public Text meterText;
	public int meterAmount = 20;
	private float meterFillAdjust;
	public string meterTextMax;
	public bool ammoNeedsReload = false;

	void Update () {

		meterFillAdjust = 0.05f * meterAmount;
		this.GetComponent<Image> ().fillAmount = meterFillAdjust;

		if (meterAmount >= 20){
			meterAmount = 20;
		}

		if (meterAmount <= 0){
			meterAmount = 0;
		}

		float meterTextDisplayed = meterAmount;
		meterText.text = meterTextDisplayed + "/" + meterTextMax;
	}

	public void CirclePause (){

		ammoNeedsReload = true;

		if (ammoNeedsReload == true) {
			this.GetComponent<Animator> ().speed = 0f;
		}
	}
	public void CircleReset() {
		this.GetComponent<Animator> ().speed = 0f;
		this.GetComponent<Animator> ().Play ("AmmoCircle", 0, 0);
	}
}
