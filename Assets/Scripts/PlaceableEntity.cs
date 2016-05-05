using UnityEngine;
using System.Collections;

public class PlaceableEntity {

	public virtual string PREFAB { get { return ""; } }

	public enum STATE {
		PLACING,
		PLACED}

	;


	public STATE state = STATE.PLACING;

}
