using UnityEngine;
using System.Collections;

public class Placing : MonoBehaviour {

	int entered = 0;

	void OnTriggerEnter (Collider collide) {
		if (collide.gameObject.CompareTag ("PlaceableEntity")) {
			Editor.S.Collide ();
			entered++;
		}
	}

	void OnTriggerExit (Collider collide) {
		if (collide.gameObject.CompareTag ("PlaceableEntity")) {
			entered--;
			if (entered == 0) {
				Editor.S.Exit ();
			}
		}
	}
}
