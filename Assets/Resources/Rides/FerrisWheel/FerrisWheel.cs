﻿using UnityEngine;
using System.Collections;

public class FerrisWheel : PlaceableEntity {

	public override string PREFAB { get { return "Rides/FerrisWheel/FerrisWheel"; } }

	public override bool placeLoop { get { return false; } }

	public override int cost { get { return 4000; } }

}
