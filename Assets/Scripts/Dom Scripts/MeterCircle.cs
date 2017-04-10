
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterCircle : MonoBehaviour {

	public Text meterText;
    public int meterMaxAmount = 20;
	public bool ammoNeedsReload = false;

    public int meterAmount;
	private float meterFillAdjust;
    private string meterTextMax;

    private void Awake()
    {
        SetMeterMaxAmount(meterMaxAmount);
    }

    void Update () {

		meterFillAdjust = meterAmount / (float) meterMaxAmount;
		this.GetComponent<Image> ().fillAmount = meterFillAdjust;

		if (meterAmount >= meterMaxAmount)
        {
			meterAmount = meterMaxAmount;
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

    public void SetMeterMaxAmount(int maxAmount)
    {
        meterMaxAmount = maxAmount;
        meterAmount = meterMaxAmount;
        meterTextMax = meterMaxAmount.ToString();
    }
}
