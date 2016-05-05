using UnityEngine;
using System.Collections;

public class Editor {

	public static Editor S;

	public enum Mode {
		VIEWING,
		PLACING}

	;

	public Mode mode = Mode.VIEWING;

	PlaceableEntity entity;
	GameObject ghost;
	bool colliding;

	Material ghostOkMat;
	Material ghostBadMat;

	public Editor () {
		S = this;
		ghostOkMat = Resources.Load ("ghostOkMat", typeof(Material)) as Material;
		ghostBadMat = Resources.Load ("ghostBadMat", typeof(Material)) as Material;
		colliding = false;
	}

	public void CreatePlaceable (PlaceableEntity placeable) {
		this.entity = placeable;
		// Load material for placing materials and the entity prefab
		GameObject goPrefab = Resources.Load (this.entity.PREFAB, typeof(GameObject)) as GameObject;
		// Instantiate the ghost prefab and set all children to use the ghost material
		this.ghost = (GameObject)MonoBehaviour.Instantiate (goPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
		this.ghost.AddComponent <Placing> ();
		foreach (Renderer rend in this.ghost.GetComponentsInChildren<Renderer>()) {
			rend.material = ghostOkMat;
		}
		this.mode = Editor.Mode.PLACING;
	}

	public void Place () {
		if (!colliding) {
			// Save the ghost object's position
			Vector3 pos = this.ghost.transform.position;
			Quaternion rot = this.ghost.transform.rotation;
			// Destroy the ghost object
			UnityEngine.Object.Destroy (this.ghost);
			// Instantiate the new game object
			GameObject goPrefab = Resources.Load (this.entity.PREFAB, typeof(GameObject)) as GameObject;
			this.ghost = (GameObject)MonoBehaviour.Instantiate (goPrefab, pos, rot);
			// Switch editing mode and reset vars
			this.mode = Editor.Mode.VIEWING;
			this.colliding = false;
			this.entity = null;
			this.ghost = null;
		}
	}

	public void TileHover (int x, int y) {
		if (this.isPlacing ()) {
			this.ghost.transform.position = Map.S.GetTile (x, y).GetGameObject ().transform.position;
		}
	}

	public void Collide () {
		foreach (Renderer rend in this.ghost.GetComponentsInChildren<Renderer>()) {
			rend.material = ghostBadMat;
			colliding = true;
		}
	}

	public void Exit () {
		foreach (Renderer rend in this.ghost.GetComponentsInChildren<Renderer>()) {
			rend.material = ghostOkMat;
			colliding = false;
		}
	}

	public void LeftClick () {
		if (isPlacing ()) {
			Place ();
		}
	}

	public void RightClick () {
		Cancel ();
	}

	public void Cancel () {
		if (ghost != null) {
			UnityEngine.Object.Destroy (this.ghost);
		}
		this.mode = Editor.Mode.VIEWING;
		this.colliding = false;
		this.entity = null;
		this.ghost = null;
	}

	public void Rotate () {
		if (isPlacing ()) {
			this.ghost.transform.Rotate (new Vector3 (0f, 90f, 0f));
		}
	}

	public bool isPlacing () {
		if (this.ghost != null && this.entity != null && this.mode == Editor.Mode.PLACING) {
			return true;
		} else {
			this.mode = Editor.Mode.VIEWING;
			return false;
		}
	}

}
