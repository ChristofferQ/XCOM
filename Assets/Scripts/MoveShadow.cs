using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShadow : MonoBehaviour
{
    public int xPos; 
    public int yPos;

    [SerializeField] private Color shadowColor;

    [SerializeField] private GridManager gridManager;

    List<Tile> walkableTiles = new List<Tile>();

    void FixedUpdate()
    {
        //FirstFuckaround();
        //SecondFuckaround();
        //ThirdFuckaround 
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

        var player = GameObject.Find("Player"); // WHO THE FUCK WOULD DO THIS!?

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

    private void ThirdFuckaround() {


    }
}
