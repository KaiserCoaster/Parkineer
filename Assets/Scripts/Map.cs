using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	
	public Texture2D terrainTiles;
	public int tileResolution;

	// ==========================

	GameObject placing;

	Texture2D[] tileTextures;
	Tile[,] tiles;

	int mapSizeX = 50;
	int mapSizeY = 50;

	Editor editor;

	void Start () {
		GenerateTextures ();
		GenerateMapData ();
		GenerateMapVisuals ();
		editor = new Editor ();
		CreatePlaceable (new FerrisWheel ());
	}

	void GenerateTextures () {
		// Pull pixel data out from the tileset to create a texture for each tile
		int numTilesPerRow = terrainTiles.width / tileResolution;
		int numRows = terrainTiles.height / tileResolution;
		Texture2D[] textures = new Texture2D[numTilesPerRow * numRows];
		for (int y = 0; y < numRows; y++) {
			for (int x = 0; x < numTilesPerRow; x++) {
				// Make a Texture for this tile texture
				Texture2D tex = new Texture2D (tileResolution, tileResolution);
				tex.SetPixels (terrainTiles.GetPixels (x * tileResolution, y * tileResolution, tileResolution, tileResolution));
				tex.filterMode = FilterMode.Bilinear;
				tex.Apply ();
				textures [y * numTilesPerRow + x] = tex;
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
		// Make a mud pit
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
		// Instatiate the ground tile prefab at the tile location
		tile.SetGameObject ((GameObject)MonoBehaviour.Instantiate (Resources.Load (Tile.prefab), Map.TileCoordToWorldCoord (tile.GetX (), tile.GetY ()), Quaternion.Euler (new Vector3 (90, 0, 0))));
		// Insert under the Map object
		tile.GetGameObject ().transform.parent = this.gameObject.transform;
		// Set the texture for this tile type.
		MeshRenderer renderer = tile.GetGameObject ().GetComponent<MeshRenderer> ();
		Texture2D tex = this.tileTextures [tile.GetTileType ()];
		renderer.material.mainTexture = tex;
		renderer.material.color = Color.white;
		// Set up tile event handler
		ClickableTile ct = tile.GetGameObject ().GetComponent<ClickableTile> ();
		ct.tileX = tile.GetX ();
		ct.tileY = tile.GetY ();
		ct.map = this;
	}

	public void MoveSelectedUnitTo (int x, int y) {
		//selectedUnit.GetComponent<Unit> ().tileX = x;
		//selectedUnit.GetComponent<Unit> ().tileY = y;
		//selectedUnit.transform.position = TileCoordToWorldCoord (x, y);
	}

	public void TileHover (int x, int y) {
		placing.transform.position = tiles [x, y].GetGameObject ().transform.position;
	}

	public void CreatePlaceable (PlaceableEntity placeable) {
		// Load material for placing materials and the entity prefab
		Material okMat = Resources.Load ("placingMat", typeof(Material)) as Material;
		GameObject goPrefab = Resources.Load (placeable.PREFAB, typeof(GameObject)) as GameObject;
		// Instantiate the prefab and set all children to use the placing material
		placing = (GameObject)MonoBehaviour.Instantiate (goPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
		foreach (Renderer rend in placing.GetComponentsInChildren<Renderer>()) {
			rend.material = okMat;
		}
	}

	public static Vector3 TileCoordToWorldCoord (int x, int y) {
		return new Vector3 (x, 0, y);
	}

}
