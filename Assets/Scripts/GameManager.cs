using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState gameState;
    public GameObject theButton;
    public GameObject theBackground;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        gameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnHeros:
                UnitManager.Instance.SpawnHeros();
                break;
            case GameState.SpawnEnemies:
                UnitManager.Instance.SpawnEnemies();
                break;
            case GameState.HerosTurn:
                UnitManager.Instance.HerosTurn();
                theButton.GetComponent<Image>().color = Color.green;
                theBackground.GetComponent<Image>().color = Color.green;
                break;
            case GameState.EnemysTurn:
                UnitManager.Instance.EnemysTurn();
                theButton.GetComponent<Image>().color = Color.red;
                theBackground.GetComponent<Image>().color = Color.red;
                break;
        }
    }
}
public enum GameState
{
    GenerateGrid = 0,
    SpawnHeros = 1,
    SpawnEnemies = 2,
    HerosTurn = 3,
    EnemysTurn = 4
}
