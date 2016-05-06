using UnityEngine;
using System.Collections;

public class UIEventHandler : MonoBehaviour {

	public void NewFerrisWheel () {
		Editor.S.CreatePlaceable (new FerrisWheel ());
	}

	public void NewPath () {
		Editor.S.CreatePlaceable (new Path ());
	}

	public void NewFence () {
		Editor.S.CreatePlaceable (new Fence ());
	}

	public void NewFenceCorner () {
		Editor.S.CreatePlaceable (new FenceCorner ());
	}

	public void NewWall () {
		Editor.S.CreatePlaceable (new Wall ());
	}

	public void NewCornerWall () {
		Editor.S.CreatePlaceable (new CornerWall ());
	}

	public void NewScrambler () {
		Editor.S.CreatePlaceable (new Scrambler ());
	}

}
