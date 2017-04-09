
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterBar : MonoBehaviour {

	public Text meterText;
	public int meterAmount = 100;
	private float meterFillAdjust;
	public string meterTextMax;

	void Update () {

		meterFillAdjust = 0.01f * meterAmount;
		this.GetComponent<Image> ().fillAmount = meterFillAdjust;

		if (meterAmount >= 100){
			meterAmount = 100;
		}

		if (meterAmount <= 0){
			meterAmount = 0;
		}

		float meterTextDisplayed = meterAmount;
		meterText.text = meterTextDisplayed + "/" + meterTextMax;
	}
}
