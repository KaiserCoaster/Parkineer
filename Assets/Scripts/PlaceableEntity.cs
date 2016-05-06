using UnityEngine;
using System.Collections;

public class PlaceableEntity {

	public virtual string PREFAB { get { return ""; } }

	public virtual bool placeLoop { get { return false; } }

	public virtual int cost { get { return 0; } }

	public enum STATE {
		PLACING,
		PLACED}

	;


	public STATE state = STATE.PLACING;

}
