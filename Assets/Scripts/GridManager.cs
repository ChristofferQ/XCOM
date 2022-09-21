using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class GridManager : MonoBehaviour
{
  [SerializeField] private int _width, _height, _depth;

  [SerializeField] private Tile _tilePrefab;

  void Start() {
    GenerateGrid();
  }

  void GenerateGrid() {
    for (int x = 0; x < _width; x++) {
        for (int y = 0; y < _height; y++) {
          for (int z = 0; z < _depth; z++) {
            var spawnedTile = Instantiate(_tilePrefab, new Vector3(x,y,z), Quaternion.identity);
            spawnedTile.name = $"Tile {x} {y} {z}";

            //Add NavMesh to each tile
            UnityEditor.AI.NavMeshBuilder.BuildNavMesh();

            //Make every 2nd tile an offset color (Only works for x and z axis)
            var isOffset = (x % 2 == 0 && z % 2 != 0) || (x % 2 != 0 && z % 2 == 0);
            spawnedTile.Init(isOffset);
            }
        }
    }
  }
}
