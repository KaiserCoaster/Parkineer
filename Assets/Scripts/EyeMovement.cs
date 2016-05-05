using UnityEngine;
using System.Collections;

public class EyeMovement : MonoBehaviour {

	public bool invertScroll;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		/////////////////////
		//keyboard scrolling

		float translationX = 0.2f * Input.GetAxis ("Horizontal");
		float translationY = 0.2f * Input.GetAxis ("Vertical");
		float fastTranslationX = 0.6f * Input.GetAxis ("Horizontal");
		float fastTranslationY = 0.6f * Input.GetAxis ("Vertical");

		if (Input.GetKey (KeyCode.LeftShift)) {
			transform.Translate (new Vector3 (fastTranslationX + fastTranslationY, 0, fastTranslationY - fastTranslationX));
		} else {
			transform.Translate (new Vector3 (translationX + translationY, 0, translationY - translationX));
		}

		//if (Input.GetKey (KeyCode.E)) {
		//transform.Rotate (new Vector3 (0, 90, 0));
		//}

		////////////////////
		//mouse scrolling
		/*
		float mousePosX = Input.mousePosition.x;
		float mousePosY = Input.mousePosition.y;
		int scrollDistance = 5;

		//Horizontal camera movement
		if (mousePosX < scrollDistance) {
			transform.Translate (new Vector3 (-1, 0, 1));
		}
		if (mousePosX >= Screen.width - scrollDistance) {
			transform.Translate (new Vector3 (1, 0, -1));
		}

		//Vertical camera movement
		if (mousePosY < scrollDistance) {
			transform.Translate (new Vector3 (-1, 0, -1));
		}
		if (mousePosY >= Screen.height - scrollDistance) {
			transform.Translate (new Vector3 (1, 0, 1));
		}
		*/

		////////////////////
		//zooming
		GameObject Eye = GameObject.Find ("Eye");


		if (Input.GetAxis ("Mouse ScrollWheel") * (invertScroll ? -1 : 1) > 0 && Eye.GetComponent<Camera> ().orthographicSize > 1) {
			Eye.GetComponent<Camera> ().orthographicSize -= 1;
		}

		if (Input.GetAxis ("Mouse ScrollWheel") * (invertScroll ? -1 : 1) < 0 && Eye.GetComponent<Camera> ().orthographicSize < 10) {
			Eye.GetComponent<Camera> ().orthographicSize += 1;
		}

		if (Input.GetKeyDown (KeyCode.Mouse2)) {
			Eye.GetComponent<Camera> ().orthographicSize = 10;
		}

	}
}
