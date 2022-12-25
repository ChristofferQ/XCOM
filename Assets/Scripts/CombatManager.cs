using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    public bool inCombat = false;

    void Start() 
    {
        Instance = this; 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && (inCombat == true))
            {
                Debug.Log("You have attacked: " + hit.collider.gameObject.name);

                //This line gives and error when rightclicking the tile under a unit
                hit.collider.gameObject.GetComponent<Unit>().TakeDamage(1);
            }
        }
    }

    public void SetCombatTiles(Vector2 pos, int attack)
    {
        Dictionary<Vector2, Tile> tiles = GridManager.Instance._tiles;
        int attackCount = 0;
        //select tiles in range
        List<Tile> area = new List<Tile>();
        area.Add(GridManager.Instance.GetTileAtPosition(pos));
        Debug.Log(area[0] + "This is start of ATTACK");
        while ( attackCount < attack)
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
                        if (nextTile.dist == -1) nextTile.dist = attackCount + 1;
                    }

                    if (GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x - 1, tilePos.y)) != null && GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x - 1, tilePos.y)).isCheck == false)
                    {
                        var nextTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x - 1, tilePos.y));
                        area.Add(nextTile);
                        nextTile.parent = tile;
                        if (nextTile.dist == -1) nextTile.dist = attackCount + 1;
                    }

                    if (GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y + 1)) != null && GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y + 1)).isCheck == false)
                    {
                        var nextTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y + 1));
                        area.Add(nextTile);
                        nextTile.parent = tile;
                        if (nextTile.dist == -1) nextTile.dist = attackCount + 1;
                    }

                    if (GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x + 1, tilePos.y -1)) != null && GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y -1)).isCheck == false)
                    {
                        var nextTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y - 1));
                        area.Add(nextTile);
                        nextTile.parent = tile;
                        if (nextTile.dist == -1) nextTile.dist = attackCount +1;
                    }

                    }

                    if (GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x + 1, tilePos.y + 1)) != null && GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x + 1, tilePos.y + 1)).isCheck == false)
                    {
                        var nextTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x + 1, tilePos.y + 1));
                        area.Add(nextTile);
                        nextTile.parent = tile;
                        if (nextTile.dist == -1) nextTile.dist = attackCount + 1;
                    }

                    if (GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x - 1, tilePos.y - 1)) != null && GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x - 1, tilePos.y - 1)).isCheck == false)
                    {
                        var nextTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x - 1, tilePos.y - 1));
                        area.Add(nextTile);
                        nextTile.parent = tile;
                        if (nextTile.dist == -1) nextTile.dist = attackCount + 1;
                    }

                    if (GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x + 1, tilePos.y - 1)) != null && GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x + 1, tilePos.y - 1)).isCheck == false)
                    {
                        var nextTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x + 1, tilePos.y - 1));
                        area.Add(nextTile);
                        nextTile.parent = tile;
                        if (nextTile.dist == -1) nextTile.dist = attackCount + 1;
                    }

                    if (GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x - 1, tilePos.y + 1)) != null && GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x - 1, tilePos.y + 1)).isCheck == false)
                    {
                        var nextTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x - 1, tilePos.y + 1));
                        area.Add(nextTile);
                        nextTile.parent = tile;
                        if (nextTile.dist == -1) nextTile.dist = attackCount + 1;
                    }
            }
            attackCount++;
        }
        //Add highlights to tiles depending on occupied or not
        foreach (Tile tile in area.ToList())
        {
            if (tile.Walkable == true && tile.Occupied == false)
            {
                tile.unitHighlight.SetActive(true);
            }

            if (tile.Occupied == true)
            {
                tile.inAttackRange = true;
                tile.CombatHightlight.SetActive(true);
            }
        }
    }

    public void CleanCombatTiles()
    {
        Dictionary<Vector2, Tile> tiles = GridManager.Instance._tiles;

        //Disable combat mode
        inCombat = false;

        //makes tiles inRange false
        foreach (Tile tile in tiles.Values)
        {
            tile.inAttackRange = false;
            tile.unitHighlight.SetActive(false);
            tile.CombatHightlight.SetActive(false);

            tile.isCheck = false;
            tile.parent = tile;
            tile.dist = -1;
        }
    }
}
