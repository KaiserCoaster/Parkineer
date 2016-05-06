using UnityEngine;
using System.Collections;

public class ClickableTile : MonoBehaviour {

	public int tileX;
	public int tileY;
	public Map map;

	void OnMouseUp () {
		
	}

	void OnMouseOver () {
		//map.HoverOver (tileX, tileY);
		//GetComponent<Renderer> ().material.color = Color.green;
	}

	void OnMouseExit () {
		//map.HoverOut (tileX, tileY);
		//GetComponent<Renderer> ().material.color = Color.white;
	}

	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitInfo;

		if (GetComponent<Collider> ().Raycast (ray, out hitInfo, Mathf.Infinity)) {
			//Debug.Log ("Tile: " + tileX + ", " + tileY);
			map.TileHover (tileX, tileY);
			Map.S.highlighter.gameObject.transform.position = Map.TileCoordToWorldCoord (tileX, tileY);
		}
	}

}
