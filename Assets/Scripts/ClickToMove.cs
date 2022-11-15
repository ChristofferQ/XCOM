using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    private Vector3 targetLocation;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    private void OnEnable() 
    {
        if (!navMeshAgent)
            navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
        this.targetLocation = transform.position;
    }

    void Update()
    {
        // Find the new targetLocation by clicking with the mouse
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (navMeshAgent.isStopped = false) return; // Already moving!  

                targetLocation = hit.collider.gameObject.transform.position;
                MovementManager.Instance.CleanMovementTiles();

            }
        }
        // If we are within stoppingDistance (defined in the navmeshagent component in the inspector), 
   // then set targetLocation to null.

        if (Vector3.Distance(transform.position, targetLocation) < navMeshAgent.stoppingDistance)
        {
            // this is not best practice, as we right now set the following every frame, 
            // if the above condition is true. It should only be set once. 
            navMeshAgent.isStopped = true;
        }
        else
        {
            // this is not best practice, as we right now set the following every frame, 
            // if the above condition is false. It should only be set once. 

            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(targetLocation);

            //PlayerManager.Instance.DeselectUnit(); // this will disable this ClickToMove script! 

        }
    }
}