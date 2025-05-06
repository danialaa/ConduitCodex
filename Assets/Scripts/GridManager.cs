using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour {
    [SerializeField] int gridWidth, gridHeight;
    [SerializeField] Tile tilePrefab;
    [SerializeField] Transform cam;

    Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();

    private void Start() {
        GenerateGrid();
    }

    void GenerateGrid() {
        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                Tile newTile = Instantiate(tilePrefab, new Vector3(x * tilePrefab.transform.localScale.x, y * tilePrefab.transform.localScale.y), Quaternion.identity);
                newTile.name = $"Tilel {x} {y}";

                bool isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);

                newTile.Init(isOffset);

                tiles[new Vector2(x, y)] = newTile;
            }
        }

        cam.transform.position = new Vector3((float)(tilePrefab.transform.localScale.x * gridWidth) / 2 - 0.5f,
            (float)(tilePrefab.transform.localScale.y * gridHeight) / 2 - 0.7f, -10);
    }

    public Tile GetTileAtPos(Vector2 pos) {
        if (tiles.TryGetValue(pos, out Tile tile)) {
            return tile;
        }

        return null;
    }
}
