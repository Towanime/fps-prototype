
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class meterCircle : MonoBehaviour {

	public Text meterText;
	public int meterAmount = 20;
	private float meterFillAdjust;
	public string meterTextMax;

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
}
