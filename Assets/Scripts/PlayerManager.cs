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

    void Awake() 
    {
        Instance = this;
        StopAllUnitsMovement();
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

        // change the selected unit to the new one. 
        this.selectedUnit = unit; 
        this.selectedUnit.GetComponent<NavMeshAgent>().SetDestination(this.selectedUnit.transform.position);
        this.selectedUnit.GetComponent<ClickToMove>().enabled = true;

        var pos = GridManager.Instance.GetCoordinateFromWorldPos(this.selectedUnit.transform.position);
        var movementSpeed = this.selectedUnit.GetComponent<Unit>().movementSpeed; 
        MovementManager.Instance.SetMovementTiles(pos, movementSpeed);
    }

    public void DeselectUnit()
    {
        if (!this.selectedUnit) return; 
        
        this.selectedUnit.GetComponent<ClickToMove>().enabled = false;
        this.selectedUnit = null;
        MovementManager.Instance.CleanMovementTiles();
    }

    void Update() 
    {
        if(Input.GetMouseButtonUp(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(ray, out hit)) {
                if(hit.collider.tag == "PlayerUnit") {
                    SelectUnit(hit.transform.gameObject);
                } else {
                    DeselectUnit();
                }
            }
        }     
    }
}
