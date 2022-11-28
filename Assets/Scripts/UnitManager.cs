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
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < NumberOfHeros; x++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(0, GridManager.Instance._width), 5, Random.Range(0,GridManager.Instance._depth));
            Instantiate(herosToSpawn, randomSpawnPosition, Quaternion.identity);
        }  
        for (int x = 0; x < NumberOfEnemies; x++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(0, GridManager.Instance._width), 5, Random.Range(0,GridManager.Instance._depth));
            Instantiate(enemiesToSpawn, randomSpawnPosition, Quaternion.identity);
        }  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnHeros()
    {
        // for (int x = 0; x < NumberOfHeros; x++)
        // {
        //     Vector3 randomSpawnPosition = new Vector3(Random.Range(0, GridManager.Instance._width), 5, Random.Range(0,GridManager.Instance._depth));
        //     Instantiate(herosToSpawn, randomSpawnPosition, Quaternion.identity);
        // }  
    }

    public void SpawnEnemys()
    {
        // for (int x = 0; x < NumberOfEnemys; x++)
        // {
        //     Vector3 randomSpawnPosition = new Vector3(Random.Range(0, GridManager.Instance._width), 5, Random.Range(0,GridManager.Instance._depth));
        //     Instantiate(enemiesToSpawn, randomSpawnPosition, Quaternion.identity);
        // }  
    }

    public void HerosTurn()
    {}

    public void EnemysTurn()
    {}

    public void PlayerTurn()
    {
        
    }
}
