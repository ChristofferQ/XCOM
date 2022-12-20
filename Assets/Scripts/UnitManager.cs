using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    public GameObject herosToSpawn;
    public int NumberOfHeros;
    public GameObject enemiesToSpawn;
    public int NumberOfEnemies;
    private static List<Tile> OccupiedTiles = new List<Tile>();
    
   
    void Awake()
    {
        Instance = this;
    }

    public void SpawnHeros()
    {
        for (int x = 0; x < NumberOfHeros; x++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(0, GridManager.Instance._width), 5, Random.Range(0, 5));
            Instantiate(herosToSpawn, randomSpawnPosition, Quaternion.identity);
        }  
        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {
        for (int x = 0; x < NumberOfEnemies; x++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(0, GridManager.Instance._width), 5, Random.Range(GridManager.Instance._depth,(GridManager.Instance._depth -5)));
            Instantiate(enemiesToSpawn, randomSpawnPosition, Quaternion.identity);
        }  
        
        GameManager.Instance.ChangeState(GameState.HerosTurn);
        Debug.Log(GameManager.Instance.gameState);
    }

    public void HerosTurn()
    {
    }

    public void EnemysTurn()
    {
    }
    
    public void ActionPoints()
    {
        //Get Units actionCount component
        //Make it do stuff when the unit takes and action ie. move or attack
        //Make it "callable" in ClickToMove and (upcoming attack script), so we can call it when an action is taken.
    }

    //Used in PlayerManager.cs --> Change this method to keep tabs on all units and the occupied tiles instead of just the currently selected
    public void DisplayUnitPosition(Vector2 pos) {
        CleanUnitTiles();
        int size = 0;
        OccupiedTiles.Add(GridManager.Instance.GetTileAtPosition(pos));
        while (size < 1)
        {
            foreach (Tile tile in OccupiedTiles.ToList() )
            {
                Vector2 tilePos = GridManager.Instance.GetCoordinateFromWorldPos(tile.transform.position);
                var unitTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y));
                OccupiedTiles.Add(unitTile);

            }
            size++;
            foreach( var x in OccupiedTiles) {
            Debug.Log("Occupied tiles: " + x.ToString());
            }

        }
        foreach (Tile tile in OccupiedTiles.ToList())
            {
                tile.unitHighlight.SetActive(true);
            }
    }

    //Used in PlayerManager.cs & ClickToMove.cs --> Change this method to keep tabs on all units and the occupied tiles instead of just the currently selected
    public void CleanUnitTiles() {
        foreach (Tile tile in OccupiedTiles) {
            tile.unitHighlight.SetActive(false);
        }
        OccupiedTiles.Clear();
    }
}
