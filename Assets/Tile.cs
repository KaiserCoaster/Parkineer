using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	GameObject go;
	int tileType;

	int x;
	int y;

	public Tile (int x, int y, int type) {
		this.x = x;
		this.y = y;
		this.tileType = type;
	}

	public void SetTileType (int type) {
		this.tileType = type;
		//update GO
	}

	public int GetTileType () {
		return tileType;
	}

	public void Create (TileMap map) {
		TileType tt = map.tileTypes [tileType];
		go = (GameObject)Instantiate (tt.tileVisualPrefab, TileMap.TileCoordToWorldCoord (x, y), Quaternion.Euler (new Vector3 (90, 0, 0)));
		ClickableTile ct = go.GetComponent<ClickableTile> ();
		ct.tileX = x;
		ct.tileY = y;
		ct.map = map;
	}

}
