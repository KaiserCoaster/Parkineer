using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour {

	public GameObject selectedUnit;

	public GameObject terrainPrefab;
	public Texture2D terrainTiles;
	public int tileResolution;

	Texture2D[] tileTextures;

	Tile[,] tiles;

	int mapSizeX = 50;
	int mapSizeY = 50;

	void Start () {
		GenerateTextures ();
		GenerateMapData ();
		GenerateMapVisuals ();
	}

	void GenerateTextures () {
		// Pull pixel data out from the tileset to create a texture for each tile
		int numTilesPerRow = terrainTiles.width / tileResolution;
		int numRows = terrainTiles.height / tileResolution;
		Texture2D[] textures = new Texture2D[numTilesPerRow * numRows];

		for (int y = 0; y < numRows; y++) {
			for (int x = 0; x < numTilesPerRow; x++) {
				textures [y * numTilesPerRow + x] = new Texture2D (tileResolution, tileResolution);
				textures [y * numTilesPerRow + x].SetPixels (terrainTiles.GetPixels (x * tileResolution, y * tileResolution, tileResolution, tileResolution));
				textures [y * numTilesPerRow + x].filterMode = FilterMode.Bilinear;
				textures [y * numTilesPerRow + x].Apply ();
			}
		}
		this.tileTextures = textures;
	}

	void GenerateMapData () {
		// Allocate Map Tiles
		tiles = new Tile[mapSizeX, mapSizeY];

		// Initialize Map Tiles
		for (int x = 0; x < mapSizeX; x++) {
			for (int y = 0; y < mapSizeY; y++) {
				tiles [x, y] = new Tile (x, y, Random.Range (0, 2));
			}
		}

		for (int x = 3; x < 8; x++) {
			for (int y = 6; y < 11; y++) {
				tiles [x, y].SetTileType (2);
			}
		}
	}

	void GenerateMapVisuals () {
		for (int x = 0; x < mapSizeX; x++) {
			for (int y = 0; y < mapSizeY; y++) {
				InstantiateTile (tiles [x, y]);
			}
		}
	}

	void InstantiateTile (Tile tile) {
		tile.SetGameObject ((GameObject)MonoBehaviour.Instantiate (this.terrainPrefab, TileMap.TileCoordToWorldCoord (tile.GetX (), tile.GetY ()), Quaternion.Euler (new Vector3 (90, 0, 0))));
		MeshRenderer renderer = tile.GetGameObject ().GetComponent<MeshRenderer> ();
		Texture2D tex = this.tileTextures [tile.GetTileType ()];
		renderer.material.mainTexture = tex;

		ClickableTile ct = tile.GetGameObject ().GetComponent<ClickableTile> ();
		ct.tileX = tile.GetX ();
		ct.tileY = tile.GetY ();
		ct.map = this;
	}

	public void MoveSelectedUnitTo (int x, int y) {
		selectedUnit.GetComponent<Unit> ().tileX = x;
		selectedUnit.GetComponent<Unit> ().tileY = y;
		selectedUnit.transform.position = TileCoordToWorldCoord (x, y);
	}

	public static Vector3 TileCoordToWorldCoord (int x, int y) {
		return new Vector3 (x, 0, y);
	}

}
