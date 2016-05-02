using UnityEngine;
using System.Collections;

public class Tile {

	GameObject go;

	int x;
	int y;
	int tileType;
	bool occupied;

	public Tile (int x, int y, int type) {
		this.x = x;
		this.y = y;
		this.tileType = type;
		this.occupied = false;
	}

	public void SetGameObject (GameObject go) {
		this.go = go;
	}

	public GameObject GetGameObject () {
		return go;
	}

	public void SetX (int x) {
		this.x = x;
	}

	public int GetX () {
		return x;
	}

	public void SetY (int y) {
		this.y = y;
	}

	public int GetY () {
		return y;
	}

	public void SetTileType (int type) {
		this.tileType = type;
		//if(go != null) {
		//	go.GetComponent<Renderer>
	}

	public int GetTileType () {
		return tileType;
	}

	public void SetOccupied (bool occupied) {
		this.occupied = occupied;
	}

	public bool IsOccupied () {
		return occupied;
	}

}
