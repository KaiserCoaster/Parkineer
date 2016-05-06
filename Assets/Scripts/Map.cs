using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	public static Map S;
	
	public Texture2D terrainTiles;
	public int tileResolution;

	public GameObject highlighter;

	// ==========================

	GameObject placing;

	Texture2D[] tileTextures;
	public Tile[,] tiles;

	int mapSizeX = 50;
	int mapSizeY = 50;

	int hoverX = 0;
	int hoverY = 0;

	Editor editor;

	void Start () {
		S = this;
		GenerateTextures ();
		GenerateMapData ();
		GenerateMapVisuals ();
		GenerateFences ();
		editor = new Editor ();
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Editor.S.LeftClick ();
		}
		if (Input.GetMouseButtonDown (1)) {
			Editor.S.RightClick ();
		}
		if (Input.GetMouseButtonDown (2)) {
			Debug.Log ("Pressed middle click.");
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			Editor.S.Rotate ();
		}
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
				textures [(numRows - y - 1) * numTilesPerRow + x] = tex;
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
				tiles [x, y] = new Tile (x, y, Random.Range (0, 3));
			}
		}
		// Make a mud pit
		/*for (int x = 3; x < 8; x++) {
			for (int y = 6; y < 11; y++) {
				tiles [x, y].SetTileType (2);
			}
		}*/
	}

	void GenerateMapVisuals () {
		for (int x = 0; x < mapSizeX; x++) {
			for (int y = 0; y < mapSizeY; y++) {
				InstantiateTile (tiles [x, y]);
			}
		}
	}

	void GenerateFences () {
		Fence fence = new Fence ();
		FenceCorner fenceCorner = new FenceCorner ();
		GameObject fencePrefab = Resources.Load (fence.PREFAB, typeof(GameObject)) as GameObject;
		GameObject fenceCornerPrefab = Resources.Load (fenceCorner.PREFAB, typeof(GameObject)) as GameObject;
		Quaternion rot = Quaternion.Euler (new Vector3 (0f, 90f, 0f));
		GameObject fences = new GameObject ("Fences");
		// Place fences along edges
		for (int x = 1; x < mapSizeX - 1; x++) {
			((GameObject)MonoBehaviour.Instantiate (fencePrefab, new Vector3 (x, 0, 0), Quaternion.identity)).transform.parent = fences.transform;
			((GameObject)MonoBehaviour.Instantiate (fencePrefab, new Vector3 (x, 0, mapSizeX - 1), Quaternion.identity)).transform.parent = fences.transform;
		}
		for (int y = 1; y < mapSizeY - 1; y++) {
			((GameObject)MonoBehaviour.Instantiate (fencePrefab, new Vector3 (0, 0, y), rot)).transform.parent = fences.transform;
			((GameObject)MonoBehaviour.Instantiate (fencePrefab, new Vector3 (mapSizeY - 1, 0, y), rot)).transform.parent = fences.transform;
		}
		// Place corner fences
		((GameObject)MonoBehaviour.Instantiate (fenceCornerPrefab, new Vector3 (0, 0, 0), Quaternion.identity)).transform.parent = fences.transform;
		((GameObject)MonoBehaviour.Instantiate (fenceCornerPrefab, new Vector3 (0, 0, mapSizeY - 1), Quaternion.Euler (new Vector3 (0f, 90f, 0f)))).transform.parent = fences.transform;
		((GameObject)MonoBehaviour.Instantiate (fenceCornerPrefab, new Vector3 (mapSizeX - 1, 0, mapSizeY - 1), Quaternion.Euler (new Vector3 (0f, 180f, 0f)))).transform.parent = fences.transform;
		((GameObject)MonoBehaviour.Instantiate (fenceCornerPrefab, new Vector3 (mapSizeX - 1, 0, 0), Quaternion.Euler (new Vector3 (0f, 270f, 0f)))).transform.parent = fences.transform;
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

	public void TileHover (int x, int y) {
		hoverX = x;
		hoverY = y;
		editor.TileHover (x, y);
	}

	public Tile GetHoveredTile () {
		return GetTile (hoverX, hoverY);
	}

	public Tile GetTile (int x, int y) {
		return tiles [x, y];
	}

	public static Vector3 TileCoordToWorldCoord (int x, int y) {
		return new Vector3 (x, 0, y);
	}

}
