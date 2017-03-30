using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dom_LoadingBar : MonoBehaviour {

	public bool isBooting = false;
	new Vector2 loadingBarDimensions;
	private float maxWidth = 1420f;
	public float barSpeed = 10f;


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
	}
}
