
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class meterBar : MonoBehaviour {

	public Text meterText;
	public float meterAmount = 1f;
	public float meterTextAdjust;
	public string meterTextMax;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		this.GetComponent<Image> ().fillAmount = meterAmount;

		if (meterAmount >= 1f){
			meterAmount = 1f;
		}

		if (meterAmount <= 0f){
			meterAmount = 0f;
		}

		float meterTextDisplayed = meterAmount * meterTextAdjust;
		meterText.text = meterTextDisplayed + meterTextMax;
	}
}
