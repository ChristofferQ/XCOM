using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    public GameObject herosToSpawn;
    public int NumberOfHeros;
    public GameObject enemiesToSpawn;
    public int NumberOfEnemies;
   
    void Awake()
    {
        Instance = this;
    }

    public void SpawnHeros()
    {
        for (int x = 0; x < NumberOfHeros; x++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(0, GridManager.Instance._width), 5, Random.Range(0,GridManager.Instance._depth));
            Instantiate(herosToSpawn, randomSpawnPosition, Quaternion.identity);
        }  
        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {
        for (int x = 0; x < NumberOfEnemies; x++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(0, GridManager.Instance._width), 5, Random.Range(0,GridManager.Instance._depth));
            Instantiate(enemiesToSpawn, randomSpawnPosition, Quaternion.identity);
        }  
    }

    public void HerosTurn()
    {
    }
}
