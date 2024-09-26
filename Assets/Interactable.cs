using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    bool isFocus = false;
    public Transform interactionTranform; 
    Transform player;
    bool hasInteractable = false;

    public virtual void Interact(){

    }
    private void Update() {
        if(isFocus && !hasInteractable){

            float distance = Vector3.Distance(transform.position, interactionTranform.position);
            if(distance < radius){
                Debug.Log("hyeeee");
                Interact();
                hasInteractable = true;
            }
        }
    }
    public void OnFocused(Transform playerTranfonms){
        isFocus = true;
        player = playerTranfonms;
        hasInteractable = false;
    }
    public void OnDeFocused(){
        isFocus = false;
        player = null;
        hasInteractable = false;
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTranform.position, radius);
    }
}
