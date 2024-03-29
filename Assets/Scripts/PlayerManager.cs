using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public GameObject selectedUnit = null; 
    [SerializeField] private List<GameObject> OwnedUnits = new List<GameObject>(); 
    public static PlayerManager Instance; 

    private static List<Unit> units = new List<Unit>();
    private bool toggleBars;

    void Awake() 
    {
        Instance = this;
        StopAllUnitsMovement();
    }

    public static void AddUnit(Unit u) 
    {
        units.Add(u);
    }

    private void StopAllUnitsMovement()
    {
        foreach(var go in OwnedUnits)
        {
            go.GetComponent<ClickToMove>().enabled = false; 
        }
    }

    private void SelectUnit(GameObject unit) 
    {
        if (!unit) return; 
        DeselectUnit();

        // disable ClickToMove script on current selectedUnit
        if (this.selectedUnit != null)
            this.selectedUnit.GetComponent<ClickToMove>().enabled = false;

        //Clean Tiles before making new
        MovementManager.Instance.CleanMovementTiles();
        UnitManager.Instance.CleanUnitTiles();
        CombatManager.Instance.CleanCombatTiles();

        // change the selected unit to the new one. 
        this.selectedUnit = unit; 

        showStatsBar(unit);
        var pos = GridManager.Instance.GetCoordinateFromWorldPos(this.selectedUnit.transform.position);
        //UnitManager.Instance.DisplayUnitTile(pos);
        UnitManager.Instance.findAllUnits();
    }

    public void DeselectUnit()
    {
        if (!this.selectedUnit) return; 
        
        this.selectedUnit.GetComponent<ClickToMove>().enabled = false;
        this.selectedUnit = null;
        MovementManager.Instance.CleanMovementTiles();
        UnitManager.Instance.CleanUnitTiles();
        CombatManager.Instance.CleanCombatTiles();
        toggleBars = false;
        for(int i = 0; i < units.Count; i++)
        {
            if (!units[i]) continue;
            units[i].inCombatRange = false;
            units[i].stats.alpha = 0f;
        }
    }

    void Update() 
    {
        //Select & Deselect Unit 
        if(Input.GetMouseButtonUp(0)) {
            if (!EventSystem.current.IsPointerOverGameObject()){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(ray, out hit)) {
                if(hit.collider.tag == "PlayerUnit" && GameManager.Instance.gameState == GameState.HerosTurn && GameManager.Instance.gameState != GameState.EnemysTurn) {
                    SelectUnit(hit.transform.gameObject);
                    Debug.Log(hit.collider.GetComponent<Unit>().name + " selected");
                } else if (hit.collider.tag == "EnemyUnit" && GameManager.Instance.gameState != GameState.HerosTurn && GameManager.Instance.gameState == GameState.EnemysTurn) {
                    SelectUnit(hit.transform.gameObject);
                    Debug.Log(hit.collider.GetComponent<Unit>().name + " selected");
                    
                } else {
                DeselectUnit();
                }
            }
        }
        }  

        //HotKeys for our HUD buttons

        if (Input.GetKeyDown("space") && PauseMenu.Instance.isPaused == false) 
        {
            end();
        }

        if(Input.GetKeyDown("1") && (this.selectedUnit != null)) 
        {
            MovementManager.Instance.performMovement();
        }

        if(Input.GetKeyDown("2") && (this.selectedUnit != null)) 
        {
            CombatManager.Instance.performCombat();
        }

        if(Input.GetKeyDown("3") && (this.selectedUnit != null)) 
        {
            Debug.Log("POWER UP BITCHES!");
        }

        if(Input.GetKeyDown("4") && (this.selectedUnit != null)) 
        {
            SelectUnit(selectedUnit);
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            toggleBars = !toggleBars;
            ShowAllStatsBar(selectedUnit);
        }
    }

    public void end()
    {
        if (GameManager.Instance.gameState == GameState.HerosTurn) {
            GameManager.Instance.ChangeState(GameState.EnemysTurn);
            Timer.Instance.ResetTimer();
            for(int i = 0; i < units.Count; i++)
            {
                units[i].actionCount = 2;
            }
            Debug.Log("Changed from Hero to Enemy turn");
            
            } else if(GameManager.Instance.gameState == GameState.EnemysTurn) {
                GameManager.Instance.ChangeState(GameState.HerosTurn);
                Timer.Instance.ResetTimer();
                Debug.Log("Changed from Enemy to Hero turn");
                for(int i = 0; i < units.Count; i++)
            {
                units[i].actionCount = 2;
            }
            } else {
                return;
            }
            DeselectUnit();
            MovementManager.Instance.CleanMovementTiles();
            CombatManager.Instance.CleanCombatTiles();
        }    
    
    public void showStatsBar(GameObject unit)
    {
        if (!this.selectedUnit) return;
        
        this.selectedUnit.GetComponent<Unit>().stats.alpha = 1.0f;
    }

    public void ShowAllStatsBar(GameObject unit)
    {
        
        if (toggleBars == true)
        {
            for(int i = 0; i < units.Count; i++)
            {
                if (!units[i]) continue;
                units[i].stats.alpha = 1f;
            }
        }
        if (toggleBars == false)
        {
            for(int i = 0; i < units.Count; i++)
            {
                if (!units[i]) continue;
                units[i].stats.alpha = 0f;
            }
        }
        showStatsBar(unit);
        if (CombatManager.Instance.inCombat == true)
        {
            CombatManager.Instance.performCombat();
        }
        

    }
}
