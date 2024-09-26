using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    Camera cam; 
    public Interactable curFocus;
    public LayerMask layerMask;
    NavMeshAgent navMeshAgent;
    Transform target;
    private void Start() {
        cam = Camera.main;
        navMeshAgent =GetComponent<NavMeshAgent>();
    }
    private void Update() {
        if(target != null){
            navMeshAgent.SetDestination(target.position);
            FaceTarget();
        }   


        if(Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100f, layerMask)) {
                Debug.Log(hit.point + hit.collider.name);   
                navMeshAgent.SetDestination(hit.point);
                RemoveFocus();
        
            }
        }
        if(Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if(interactable != null) {
                    SetFocus(interactable);
                }
            }
        }
    }
    void SetFocus(Interactable interactable){
        if(interactable != curFocus){
            if(curFocus != null) curFocus.OnDeFocused();
            curFocus = interactable;;
            FollowerTarget(interactable);
        }
        interactable.OnFocused(transform);
       

    }
    void RemoveFocus(){

        if(curFocus != null) curFocus.OnDeFocused();
        curFocus = null;
        StopFollowerTarget();
    }

    void FollowerTarget(Interactable target){
        navMeshAgent.stoppingDistance = target.radius * .8f;
        navMeshAgent.updatePosition = true ;
        this.target = target.interactionTranform;
    }

    void StopFollowerTarget(){
        navMeshAgent.stoppingDistance = 0f;
        navMeshAgent.updatePosition = false;
        this.target = null;
    }
    void FaceTarget(){
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*5f);
    }
}
