using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class GridManager : MonoBehaviour
{
  [SerializeField] private int _width, _height, _depth;
  [SerializeField] private Tile _tilePrefab;

  private Dictionary<Vector2, Tile> _tiles;

  void Start() {
    GenerateGrid();
  }

  void GenerateGrid() {
    _tiles = new Dictionary<Vector2, Tile>();
    for (int x = 0; x < _width; x++) {
        for (int y = 0; y < _height; y++) {
          for (int z = 0; z < _depth; z++) {
            var spawnedTile = Instantiate(_tilePrefab, new Vector3(x,y,z), Quaternion.identity);
            spawnedTile.name = $"Tile {x} {y} {z}";

            //Make every 2nd tile an offset color (Only works for x and z axis)
            var isOffset = (x % 2 == 0 && z % 2 != 0) || (x % 2 != 0 && z % 2 == 0);
            spawnedTile.Init(isOffset);


            _tiles[new Vector2(x,z)] = spawnedTile;
            }   
        }
    }
    //Add NavMesh
    UnityEditor.AI.NavMeshBuilder.BuildNavMesh();    
  }

  public Tile GetTileAtPosition(Vector2 pos) {
    if(_tiles.TryGetValue(pos, out var tile)) {
      return tile;
    }
    return null;
  } 

  public Dictionary<Vector2, Tile> GetTileMap()
  {
    return this._tiles;
  }
}
