using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShadow : MonoBehaviour
{
    public int xPos; 
    public int yPos;

    [SerializeField] private Color shadowColor;

    [SerializeField] private GridManager gridManager;
    GameObject player; 
    List<Tile> walkableTiles = new List<Tile>();

    void Awake() 
    {
        player = GameObject.Find("Player"); // this should only be added to the active (selected) player; 
        
    }

    void FixedUpdate()
    {
        //FirstFuckaround();
        SecondFuckaround();
        //GeneratePathFindingGraph(); 
    } 

    private void FirstFuckaround() 
    {
        
        walkableTiles = new List<Tile>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                var tile = gridManager.GetTileAtPosition(new Vector2(x + xPos, y + yPos));
                if (tile == null) continue;
                if (walkableTiles.Contains(tile)) continue; 
               // Debug.Log(tile);
                //if (!tile.walkable) continue; 

                walkableTiles.Add(tile);
            }
        } 
        Debug.Log(walkableTiles.Count);


        foreach(Tile t in walkableTiles)
        {
            t.GetComponent<Renderer>().material.color = shadowColor;
        }
    }

    private void SecondFuckaround()
    {
        walkableTiles = new List<Tile>();


        var playerPosRaw = new Vector2(
            Mathf.Floor(player.transform.position.x),
            Mathf.Floor(player.transform.position.z)
        );
        
        //var playerPos = gridManager.GetTileAtPosition(playerPosRaw);

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {

                var tile = gridManager.GetTileAtPosition(new Vector2(x + playerPosRaw.x, y + playerPosRaw.y));
                if (tile == null) continue;
                if (walkableTiles.Contains(tile)) continue; 
               // Debug.Log(tile);
                //if (!tile.walkable) continue; 

                walkableTiles.Add(tile);
            }
        } 
        Debug.Log(walkableTiles.Count);


        foreach(Tile t in walkableTiles)
        {
            t.GetComponent<Renderer>().material.color = shadowColor;
        }
    }

    /*
    //Change this to GeneratePathFindingGraph()
    private void GeneratePathFindingGraph() 
    {
        graph = new Node[mapSizeX,mapSizeY];

        for(int x = 0; x < mapSizeX; x++) {
            for(int y = 0; y < mapSizeY; y++)

            //4 way diretional movement
            //Add x,y + and x,y - for diagonal movement
            if(x > 0)
            graph[x,y].neighbours.Add(graph[x-1,y]);
            if(x < mapSizeX -1)
            graph[x,y].neighbours.Add(graph[x+1,y]);
            if(y > 0)
            graph[x,y].neighbours.Add(graph[x,y-1]);
            if(y < mapSizeY -1)
            graph[x,y].neighbours.Add(graph[x,y+1]);

            //Something to limit range ie. speed++ or count++

            //Something to color the walkable tiles maybe??
            
        }
    }
    */
}
