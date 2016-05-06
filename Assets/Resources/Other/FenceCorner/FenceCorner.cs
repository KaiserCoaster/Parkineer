using UnityEngine;
using System.Collections;

public class FenceCorner : PlaceableEntity {

	public override string PREFAB { get { return "Other/FenceCorner/FenceCorner"; } }

	public override bool placeLoop { get { return true; } }

}
