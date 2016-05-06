using UnityEngine;
using System.Collections;

public class Path : PlaceableEntity {

	public override string PREFAB { get { return "Other/Path/Path"; } }

	public override bool placeLoop { get { return true; } }

	public override int cost { get { return 15; } }

}
