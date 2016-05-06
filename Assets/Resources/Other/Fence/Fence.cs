using UnityEngine;
using System.Collections;

public class Fence : PlaceableEntity {

	public override string PREFAB { get { return "Other/Fence/Fence"; } }

	public override bool placeLoop { get { return true; } }

}
