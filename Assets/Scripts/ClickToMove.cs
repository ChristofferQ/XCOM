using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour

{
    private Vector3 targetLocation;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
                targetLocation = hit.collider.gameObject.transform.position;
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
        }
    }
}