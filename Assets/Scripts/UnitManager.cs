using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    public GameObject herosToSpawn;
    public int NumberOfHeros;
    public GameObject enemiesToSpawn;
    public int NumberOfEnemies;
    public GameObject chestsToSpawn;
    public int NumberOfChests;
    private static List<Tile> OccupiedTiles = new List<Tile>();
    public List<GameObject> AllUnits = new List<GameObject>();
    public List<GameObject> PlayerUnits = new List<GameObject>();
    public List<GameObject> EnemyUnits = new List<GameObject>();
    public List<GameObject> Chests = new List<GameObject>();
    public List<GameObject> Props = new List<GameObject>();
    private static List<Tile> OccupiedTiles2 = new List<Tile>();
    private static List<Tile> OccupiedTiles3 = new List<Tile>();
    private static List<Tile> Selected = new List<Tile>();
   
    void Awake()
    {
        Instance = this;
    }

    public void SpawnHeros()
    {
        for (int x = 0; x < NumberOfHeros; x++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(0, GridManager.Instance._width), 5, Random.Range(0, 5));
            var Hero = Instantiate(herosToSpawn, randomSpawnPosition, Quaternion.identity);
            Hero.name = $"Hero {x + 1}";
            //spawnedTile.name = $"Tile {x} {y} {z}";
        }  
        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }

    

    public void SpawnEnemies()
    {
        for (int x = 0; x < NumberOfEnemies; x++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(0, GridManager.Instance._width), 5, Random.Range(GridManager.Instance._depth,(GridManager.Instance._depth -5)));
            var Enemy =Instantiate(enemiesToSpawn, randomSpawnPosition, Quaternion.Euler(0,180f,0));
            Enemy.name = $"Enemy {x + 1}";
        }  
        
        GameManager.Instance.ChangeState(GameState.SpawnChests);
        Debug.Log(GameManager.Instance.gameState);
    }
    public void SpawnChests()
    {
        for (int x = 0; x < NumberOfChests; x++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(0, GridManager.Instance._width),0.6f, Random.Range(5,(GridManager.Instance._depth -5)));
            var Chest =Instantiate(chestsToSpawn, randomSpawnPosition, Quaternion.Euler(0,180f,0));
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
    public void DisplayUnitTile(Vector2 pos) {
        CleanUnitTiles();
        int size = 0;
        Selected.Add(GridManager.Instance.GetTileAtPosition(pos));
        while (size < 1)
        {
            foreach (Tile tile in Selected.ToList() )
            {
                Vector2 tilePos = GridManager.Instance.GetCoordinateFromWorldPos(tile.transform.position);
                var unitTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y));
                Selected.Add(unitTile);
            }
            size++;
        }
        foreach (Tile tile in Selected.ToList())
            {
                tile.unitHighlight.SetActive(true);
            }
    }

    //Used in PlayerManager.cs & ClickToMove.cs --> Change this method to keep tabs on all units and the occupied tiles instead of just the currently selected
    public void CleanUnitTiles() {

        foreach (Tile tile in OccupiedTiles) {
            tile.EnemyHighlight.SetActive(false);
            tile.Occupied = false;
            tile.Walkable = true;
        }
        foreach (Tile tile in OccupiedTiles2) {
            tile.PlayerHighlight.SetActive(false);
            tile.Occupied = false;
            tile.Walkable = true;
        }
        foreach (Tile tile in OccupiedTiles3) {
            tile.PlayerHighlight.SetActive(false);
            tile.Occupied = false;
            tile.Walkable = true;
        }
        OccupiedTiles.Clear();
        OccupiedTiles2.Clear();
        OccupiedTiles3.Clear();
    }

    public void findAllUnits() {
        EnemyUnits.AddRange(GameObject.FindGameObjectsWithTag("EnemyUnit"));
        foreach( var element in EnemyUnits.ToList()) {
            if (!element) continue;
            Vector2 tilePos = GridManager.Instance.GetCoordinateFromWorldPos(element.transform.position);
            var unitTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y));
            OccupiedTiles.Add(unitTile);
        }
        
        foreach (Tile tile in OccupiedTiles.ToList())
        {
            tile.EnemyHighlight.SetActive(true);
            tile.Occupied = true;
            tile.Walkable = false;
        }
        PlayerUnits.AddRange(GameObject.FindGameObjectsWithTag("PlayerUnit"));
        foreach( var element in PlayerUnits.ToList()) {
            if (!element) continue;
            Vector2 tilePos2 = GridManager.Instance.GetCoordinateFromWorldPos(element.transform.position);
            var unitTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos2.x, tilePos2.y));
            OccupiedTiles2.Add(unitTile);
        }
         foreach (Tile tile in OccupiedTiles2.ToList())
        {
            tile.PlayerHighlight.SetActive(true);
            tile.Occupied = true;
            tile.Walkable = false;
        }
        Chests.AddRange(GameObject.FindGameObjectsWithTag("Chest"));
        foreach( var element in Chests.ToList()) {
            if (!element) continue;
            Vector2 tilePos2 = GridManager.Instance.GetCoordinateFromWorldPos(element.transform.position);
            Debug.Log(tilePos2 + element.name + "Check this out");
            var unitTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos2.x, tilePos2.y));
            OccupiedTiles3.Add(unitTile);
        }
         foreach (Tile tile in OccupiedTiles3.ToList())
        {
            tile.unitHighlight.SetActive(true);
            tile.Occupied = true;
            tile.Walkable = false;
        }
        Props.AddRange(GameObject.FindGameObjectsWithTag("Prop"));
        foreach( var element in Props.ToList()) {
            if (!element) continue;
            Vector2 tilePos2 = GridManager.Instance.GetCoordinateFromWorldPos(element.transform.position);
            var unitTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos2.x, tilePos2.y));
            OccupiedTiles3.Add(unitTile);
        }
         foreach (Tile tile in OccupiedTiles3.ToList())
        {
            tile.unitHighlight.SetActive(true);
            tile.Occupied = true;
            tile.Walkable = false;
        }
        EnemyUnits.Clear();
        PlayerUnits.Clear();
        Chests.Clear();
        Props.Clear();
    }
}
