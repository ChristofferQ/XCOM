using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public GameObject selectedUnit = null; 
    [SerializeField] private List<GameObject> OwnedUnits = new List<GameObject>(); 
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

        // disable ClickToMove script on current selectedUnit
        if (this.selectedUnit != null)
            this.selectedUnit.GetComponent<ClickToMove>().enabled = false;

        //Clean Movement Tiles before making new
        MovementManager.Instance.CleanMovementTiles();
        UnitManager.Instance.CleanUnitTiles();
        CombatManager.Instance.CleanCombatTiles();

        // change the selected unit to the new one. 
        this.selectedUnit = unit; 
        this.selectedUnit.GetComponent<NavMeshAgent>().SetDestination(this.selectedUnit.transform.position);
        this.selectedUnit.GetComponent<ClickToMove>().enabled = true;

        showStatsBar(unit);
        var pos = GridManager.Instance.GetCoordinateFromWorldPos(this.selectedUnit.transform.position);
        var movementSpeed = this.selectedUnit.GetComponent<Unit>().movementSpeed; 
        
        MovementManager.Instance.SetMovementTiles(pos, movementSpeed);
        UnitManager.Instance.findAllUnits();
        //UnitManager.Instance.DisplayUnitTile(pos);
    }

    public void DeselectUnit()
    {
        if (!this.selectedUnit) return; 
        
        this.selectedUnit.GetComponent<ClickToMove>().enabled = false;
        this.selectedUnit = null;
        MovementManager.Instance.CleanMovementTiles();
        UnitManager.Instance.CleanUnitTiles();
        CombatManager.Instance.CleanCombatTiles();

    }

    public void performCombat() 
    {
        if (!this.selectedUnit) return;
        
        var pos = GridManager.Instance.GetCoordinateFromWorldPos(this.selectedUnit.transform.position);
        var attackRange = this.selectedUnit.GetComponent<Unit>().attackRange;
        MovementManager.Instance.CleanMovementTiles();
        CombatManager.Instance.SetCombatTiles(pos, attackRange);
        CombatManager.Instance.inCombat = true;

        var radius = 1;
        var center = selectedUnit.transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach(var hitCollider in hitColliders)
        {
            //Debug.Log("You have found me!");
            //Debug.Log("Hero: " + hitCollider.name);
            hitCollider.GetComponent<Unit>().inCombatRange = true;
        }
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
        if (Input.GetKeyDown("space")) 
        {
            end();
        }

        if(Input.GetKeyDown("g") && (this.selectedUnit != null)) {
            performCombat();
            //Debug.Log(CombatManager.Instance.inCombat);
            //CombatManager.Instance.inCombat = true;
            //Debug.Log(CombatManager.Instance.inCombat);
        }


    }

    public void end()
    {
        if (GameManager.Instance.gameState == GameState.HerosTurn) {
            GameManager.Instance.ChangeState(GameState.EnemysTurn);
            for(int i = 0; i < units.Count; i++)
            {
                units[i].actionCount = 2;
            }
            Debug.Log("Changed from Hero to Enemy turn");
            
            } else if(GameManager.Instance.gameState == GameState.EnemysTurn) {
                GameManager.Instance.ChangeState(GameState.HerosTurn);
                for(int i = 0; i < units.Count; i++)
                {
                units[i].actionCount = 2;
                }
                Debug.Log("Changed from Enemy to Hero turn");

            } else {
                return;
            }
        }    
    
    public void showStatsBar(GameObject unit)
    {
        if (!this.selectedUnit) return;
        
        var test = this.selectedUnit.GetComponent<Unit>();
        test.stats.alpha = 1.0f;
    }
}
