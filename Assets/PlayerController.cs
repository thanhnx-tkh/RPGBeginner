using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    Camera cam; 
    public LayerMask layerMask;
    NavMeshAgent navMeshAgent;
    private void Start() {
        cam = Camera.main;
        navMeshAgent =GetComponent<NavMeshAgent>();
    }
    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                Debug.Log(hit.point + hit.collider.name);   
                navMeshAgent.SetDestination(hit.point);
            }

        }
    }
    
}
