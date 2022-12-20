using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject selectedUnit = null; 
    [SerializeField] private List<GameObject> OwnedUnits = new List<GameObject>(); 
    public bool isPerformingAction = false;
    public static PlayerManager Instance; 

    private static List<Unit> units = new List<Unit>();

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
        if (isPerformingAction) return;

        // disable ClickToMove script on current selectedUnit
        if (this.selectedUnit != null)
            this.selectedUnit.GetComponent<ClickToMove>().enabled = false;

        //Clean Movement Tiles before making new
        MovementManager.Instance.CleanMovementTiles();
        UnitManager.Instance.CleanUnitTiles();

        // change the selected unit to the new one. 
        this.selectedUnit = unit; 
        this.selectedUnit.GetComponent<NavMeshAgent>().SetDestination(this.selectedUnit.transform.position);
        this.selectedUnit.GetComponent<ClickToMove>().enabled = true;

        var pos = GridManager.Instance.GetCoordinateFromWorldPos(this.selectedUnit.transform.position);
        var movementSpeed = this.selectedUnit.GetComponent<Unit>().movementSpeed; 
        MovementManager.Instance.SetMovementTiles(pos, movementSpeed);
        UnitManager.Instance.DisplayUnitTile(pos);
        
    }

    public void DeselectUnit()
    {
        if (!this.selectedUnit) return; 
        
        this.selectedUnit.GetComponent<ClickToMove>().enabled = false;
        this.selectedUnit = null;
        MovementManager.Instance.CleanMovementTiles();
        UnitManager.Instance.CleanUnitTiles();
    }

    void Update() 
    {

        //Select & Deselect Unit 
        if(Input.GetMouseButtonUp(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(ray, out hit)) {
                if(hit.collider.tag == "PlayerUnit" && GameManager.Instance.gameState == GameState.HerosTurn && GameManager.Instance.gameState != GameState.EnemysTurn) {
                    SelectUnit(hit.transform.gameObject);
                    Debug.Log("Hero Selected");
                } else if (hit.collider.tag == "EnemyUnit" && GameManager.Instance.gameState != GameState.HerosTurn && GameManager.Instance.gameState == GameState.EnemysTurn) {
                    SelectUnit(hit.transform.gameObject);
                    Debug.Log("Enemy Selected");
                    
                } else {
                DeselectUnit();
                }
            }
        }  

            //Simple way to change players in testing, should be rewritten/changed later.
            if (Input.GetKeyDown("space")) {
            if (GameManager.Instance.gameState == GameState.HerosTurn) {
                GameManager.Instance.ChangeState(GameState.EnemysTurn);
                for(int i = 0; i < units.Count; i++)
                {
                    units[i].actionCount = 2;
                    Debug.Log(units);
                }
                //GetComponent<Unit>().actionCount = 2;
                Debug.Log("Changed from Hero to Enemy turn");
            } else if(GameManager.Instance.gameState == GameState.EnemysTurn) {
                GameManager.Instance.ChangeState(GameState.HerosTurn);
                Debug.Log("Changed from Enemy to Hero turn");
            } else {
                return;
            }
            
        }       
    }
}
