using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour {

	public GameObject selectedUnit;

	public TileType[] tileTypes;

	Tile[,] tiles;

	int mapSizeX = 80;
	int mapSizeY = 80;

	void Start () {
		GenerateMapData ();
		GenerateMapVisuals ();
	}

	void GenerateMapData () {
		// Allocate Map Tiles
		tiles = new Tile[mapSizeX, mapSizeY];

		// Initialize Map Tiles
		for (int x = 0; x < mapSizeX; x++) {
			for (int y = 0; y < mapSizeY; y++) {
				tiles [x, y] = new Tile (x, y, 0);
			}
		}
		tiles [3, 6].SetTileType (1);
	}

	void GenerateMapVisuals () {
		for (int x = 0; x < mapSizeX; x++) {
			for (int y = 0; y < mapSizeY; y++) {
				tiles [x, y].Create (this);
			}
		}
	}

	public static Vector3 TileCoordToWorldCoord (int x, int y) {
		return new Vector3 (x, 0, y);
	}

	public void MoveSelectedUnitTo (int x, int y) {
		selectedUnit.GetComponent<Unit> ().tileX = x;
		selectedUnit.GetComponent<Unit> ().tileY = y;
		selectedUnit.transform.position = TileCoordToWorldCoord (x, y);
	}

}
