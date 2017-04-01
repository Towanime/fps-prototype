using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dom_LoadingBar : MonoBehaviour {

	public bool isBooting = false;
	Vector2 loadingBarDimensions;
	private float maxWidth = 1420f;
	public float barSpeed = 10f;
	public float alphaSpeed = 10f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		loadingBarDimensions = new Vector2 (this.GetComponent<RectTransform> ().sizeDelta.x, this.GetComponent<RectTransform> ().sizeDelta.y);

		if (isBooting == true && loadingBarDimensions.x < maxWidth) {

			loadingBarDimensions.x += Time.deltaTime * barSpeed;
			this.GetComponent<RectTransform> ().sizeDelta = new Vector2 (loadingBarDimensions.x, loadingBarDimensions.y);
		}

		if (loadingBarDimensions.x >= maxWidth) {

			Image loadingBarGraphic = this.GetComponent <Image>();
			Color loadingBarColor = loadingBarGraphic.color;

			loadingBarColor.a -= alphaSpeed * Time.deltaTime; 

			loadingBarGraphic.color = loadingBarColor;

			if (loadingBarColor.a <= 0) {

				loadingBarColor.a = 0;
				isBooting = false;
			}
		}
	}
}
