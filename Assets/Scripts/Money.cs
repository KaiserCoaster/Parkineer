using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Money : MonoBehaviour {

	public static Money S;

	Text cashText;

	int cash = 20000;

	// Use this for initialization
	void Start () {
		S = this;
		cashText = GetComponent<Text> ();
		cashText.text = cash.ToString ();
	}

	public void Charge (int amt) {
		cash -= amt;
		cashText.text = cash.ToString ();
	}

}
