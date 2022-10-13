using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnit : MonoBehaviour
{

    public static GameObject controlledUnit = null;
    


    void Start()
    {  
        GameObject.Find("Player").GetComponent<ClickToMove>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(ray, out hit)) {
                if(hit.collider.tag == "Player") {
                    controlledUnit = hit.transform.gameObject;
                    controlledUnit.GetComponent<ClickToMove>().enabled = true;
                    Debug.Log(gameObject);
                } else {
                GameObject.Find("Player").GetComponent<ClickToMove>().enabled = false;
                controlledUnit = null;
                }
            }
        }     
    }
}
