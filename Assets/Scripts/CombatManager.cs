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
        if (Input.GetMouseButtonDown(1) && (inCombat == true))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                var unit = PlayerManager.Instance.selectedUnit.GetComponent<Unit>();
                if ((hit.collider.gameObject.GetComponent<Unit>()) && (hit.collider.gameObject.GetComponent<Unit>().inCombatRange == true) )
                {
                    Debug.Log("You have attacked: " + hit.collider.gameObject.name);
                    hit.collider.gameObject.GetComponent<Unit>().TakeDamage(20);
                    unit.actionCount--;

                } else {
                    Debug.Log("You have attacked an invalid target");
                }
            
            } else {
                Debug.Log("No Target!");
            }
            PlayerManager.Instance.DeselectUnit();
        }
    }

    public void performCombat() 
    {
        var unit = PlayerManager.Instance.selectedUnit.GetComponent<Unit>();
        if (!unit) return;

        if (unit.actionCount > 0)
        { 
            var pos = GridManager.Instance.GetCoordinateFromWorldPos(unit.transform.position);
            var attackRange = unit.attackRange;
            MovementManager.Instance.CleanMovementTiles();
            CombatManager.Instance.SetCombatTiles(pos, attackRange);
            CombatManager.Instance.inCombat = true;

            var radius = 1;
            var center = unit.transform.position;
            Collider[] hitColliders = Physics.OverlapSphere(center, radius);
            foreach(var hitCollider in hitColliders)
            {
                hitCollider.GetComponent<Unit>().inCombatRange = true;
            }
        } else {
            Debug.Log("Out of actions");
        }
    }

    public void SetCombatTiles(Vector2 pos, int attack)
    {
        Dictionary<Vector2, Tile> tiles = GridManager.Instance._tiles;
        int attackCount = 0;
        //select tiles in range
        List<Tile> area = new List<Tile>();
        area.Add(GridManager.Instance.GetTileAtPosition(pos));
        //Debug.Log(area[0] + "This is start of ATTACK");
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

    public void GameOver()
    {

    }
}
