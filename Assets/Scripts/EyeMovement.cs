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

		//rotate
		if (Input.GetKeyUp (KeyCode.E)) {
			transform.RotateAround (Map.S.GetHoveredTile ().GetGameObject ().transform.position, Vector3.up, -90f); 
		}
		if (Input.GetKeyUp (KeyCode.Q)) {
			transform.RotateAround (Map.S.GetHoveredTile ().GetGameObject ().transform.position, Vector3.up, 90f); 
		}

		//zooming
		GameObject Eye = GameObject.Find ("Eye");


		if (Input.GetAxis ("Mouse ScrollWheel") * (invertScroll ? -1 : 1) > 0 && Eye.GetComponent<Camera> ().orthographicSize > 2) {
			Eye.GetComponent<Camera> ().orthographicSize -= 1;
		}

		if (Input.GetAxis ("Mouse ScrollWheel") * (invertScroll ? -1 : 1) < 0 && Eye.GetComponent<Camera> ().orthographicSize < 10) {
			Eye.GetComponent<Camera> ().orthographicSize += 1;
		}

		if (Input.GetKeyDown (KeyCode.Mouse2)) {
			Eye.GetComponent<Camera> ().orthographicSize = 7;
		}

	}
}
