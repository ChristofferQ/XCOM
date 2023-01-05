using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class GridManager : MonoBehaviour
{
  public int _width, _height, _depth;
  [SerializeField] private Tile _tilePrefab;
  [SerializeField] private GameObject wallPrefab;
  [SerializeField] private GameObject prop1;
  [SerializeField] private GameObject prop2;
  [SerializeField] private GameObject prop3;
  [SerializeField] private GameObject wallprop1;
  [SerializeField] private GameObject wallprop2;

  [SerializeField] private bool walls;
  [SerializeField] private bool props;

  public static GridManager Instance;

  public Dictionary<Vector2, Tile> _tiles;

  void Awake() {
    Instance = this; 
    //GenerateGrid();
  }

  public void GenerateGrid() {

    _tiles = new Dictionary<Vector2, Tile>();
    
    for (int x = 0; x < _width; x++) {
        for (int y = 0; y < _height; y++) {
          for (int z = 0; z < _depth; z++) {
            if(walls == true)
            {
              if (z == 0)
              {
                var spawnedWall = Instantiate(wallPrefab, new Vector3(x,y+0.5f,z-0.5f), Quaternion.identity);
              }
              if (z == _depth-1)
              {
                var spawnedWall = Instantiate(wallPrefab, new Vector3(x,y+0.5f,z+0.5f), Quaternion.Euler(0,180f,0));
              }
              if (x == 0)
              {
                var spawnedWall = Instantiate(wallPrefab, new Vector3(x-0.5F,y+0.5f,z), Quaternion.Euler(0,90f,0));
              }
              if (x == _width-1)
              {
                var spawnedWall = Instantiate(wallPrefab, new Vector3(x+0.5F,y+0.5f,z), Quaternion.Euler(0,-90f,0));
              }
            }

            var spawnedTile = Instantiate(_tilePrefab, new Vector3(x,y,z), Quaternion.identity);
            spawnedTile.name = $"Tile {x} {y} {z}";

            //Make every 2nd tile an offset color (Only works for x and z axis)
            var isOffset = (x % 2 == 0 && z % 2 != 0) || (x % 2 != 0 && z % 2 == 0);
            spawnedTile.Init(isOffset);

            _tiles[new Vector2(x,z)] = spawnedTile;
            }   
        }
    }
    spawnProps();

    //Add NavMesh
    UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
    GameManager.Instance.ChangeState(GameState.SpawnHeros);// Change to spawnheros    
  }

  private void spawnProps()
  {
    if (props == true)
    {
      for(var i = 0; i < 5; i++)
      {
        var spawnedProp1 = Instantiate(prop1, new Vector3(Random.Range(0, _width),0.5f,Random.Range(0, _depth)), Quaternion.Euler(0,Random.Range(0,360),0));
        var spawnedProp2 = Instantiate(prop2, new Vector3(Random.Range(0, _width),1,Random.Range(0, _depth)), Quaternion.Euler(0,Random.Range(0,360),0));
        var spawnedProp3 = Instantiate(prop3, new Vector3(Random.Range(1, _width -1),0.5f,Random.Range(5, _depth -5)), Quaternion.identity);
        var spawnedWallProp1 = Instantiate(wallprop1, new Vector3(Random.Range(0, _width),4,-0.5f), Quaternion.identity);
       // var spawnedWallProp2 = Instantiate(wallprop2, new Vector3(-0.46f,3.5f,Random.Range(0, _depth)), Quaternion.Euler(0,90,0));
      }
    }
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

  public Vector2 GetCoordinateFromWorldPos(Vector3 pos)
  {
    return GetCoordinateFromWorldPos(new Vector2(pos.x, pos.z));
  }

  public Vector2 GetCoordinateFromWorldPos(Vector2 pos) 
  {
    return new Vector2(
            Mathf.Floor(pos.x),
            Mathf.Floor(pos.y)
        );
  }
}
