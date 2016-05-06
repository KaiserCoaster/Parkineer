using UnityEngine;
using System.Collections;

public class Scrambler : PlaceableEntity {

	public override string PREFAB { get { return "Rides/Scrambler/Scrambler"; } }

	public override bool placeLoop { get { return false; } }

}
