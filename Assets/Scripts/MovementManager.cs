using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public static MovementManager Instance;

    void Start()
    {
        Instance = this; 
    }

    public void SetMovementTiles(Vector2 pos, int movement)
    {
        Dictionary<Vector2, Tile> tiles = GridManager.Instance._tiles;
        int moveCount = 0;
        //select tiles in range
        List<Tile> area = new List<Tile>();
        area.Add(GridManager.Instance.GetTileAtPosition(pos));
        Debug.Log(area[0] + "This is start");
        while ( moveCount < movement)
        {
            foreach (Tile tile in area.ToList() )
            {
                //movement BFS
                Vector2 tilePos = GridManager.Instance.GetCoordinateFromWorldPos(tile.transform.position);
                
                if (tile.Walkable == true || tilePos == pos && tile.isCheck == false)
                {
                    
                    // add for directions
                    if (GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x + 1, tilePos.y)) != null && GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x + 1, tilePos.y)).isCheck == false)
                    {
                        var nextTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x + 1, tilePos.y));
                        area.Add(nextTile);
                        nextTile.parent = tile;
                        if (nextTile.dist == -1) nextTile.dist = moveCount + 1;
                    }

                    if (GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x - 1, tilePos.y)) != null && GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x - 1, tilePos.y)).isCheck == false)
                    {
                        var nextTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x - 1, tilePos.y));
                        area.Add(nextTile);
                        nextTile.parent = tile;
                        if (nextTile.dist == -1) nextTile.dist = moveCount + 1;
                    }

                    if (GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y + 1)) != null && GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y + 1)).isCheck == false)
                    {
                        var nextTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y + 1));
                        area.Add(nextTile);
                        nextTile.parent = tile;
                        if (nextTile.dist == -1) nextTile.dist = moveCount + 1;
                    }

                    if (GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x + 1, tilePos.y -1)) != null && GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y -1)).isCheck == false)
                    {
                        var nextTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y - 1));
                        area.Add(nextTile);
                        nextTile.parent = tile;
                        if (nextTile.dist == -1) nextTile.dist = moveCount +1;
                    }
                    tile.isCheck = true;
                }
            }
            moveCount++;
        }
        //makes tiles inRange true
        foreach (Tile tile in area.ToList())
        {
            if (tile.Walkable == true)
            {
                tile.inRange = true;
                tile.rangeHighlight.SetActive(true);

                tile.isCheck = true;
            }
        }
    }

    public void CleanMovementTiles()
    {
        Dictionary<Vector2, Tile> tiles = GridManager.Instance._tiles;

        //makes tiles inRange false
        foreach (Tile tile in tiles.Values)
        {
            tile.inRange = false;
            tile.rangeHighlight.SetActive(false);

            tile.isCheck = false;
            tile.parent = tile;
            tile.dist = -1;
        }

    }


}
