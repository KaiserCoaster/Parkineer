using UnityEngine;
using System.Collections;

public class Wall : PlaceableEntity {

	public override string PREFAB { get { return "Other/Wall/Wall"; } }

	public override bool placeLoop { get { return true; } }

}
