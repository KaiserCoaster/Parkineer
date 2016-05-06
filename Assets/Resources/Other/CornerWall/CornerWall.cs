using UnityEngine;
using System.Collections;

public class CornerWall : PlaceableEntity {

	public override string PREFAB { get { return "Other/CornerWall/CornerWall"; } }

	public override bool placeLoop { get { return true; } }

	public override int cost { get { return 5; } }

}
