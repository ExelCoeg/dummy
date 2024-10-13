using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerInputActions playerInputActions;
    public InputAction move;
    public InputAction interact;
    Vector3 moveDirection;
    RaycastHit hit;
    public InteractableObject currentInteractableObject;
    public GameObject uiInteract;
    [SerializeField] private float currentSpeed = 5f;
    // Start is called before the first frame update
   private void Awake() {
        playerInputActions = new PlayerInputActions();
   }
   private void Update() {
        Interact();
   }
    private void FixedUpdate() {
        moveDirection = move.ReadValue<Vector3>();// movement
        moveDirection = moveDirection.x * transform.right + moveDirection.z * transform.forward;
        transform.position += moveDirection * currentSpeed * Time.fixedDeltaTime;
    }

    private void OnEnable(){
        
        move = playerInputActions.Player.Move;
        interact = playerInputActions.Player.Interact;
        
        interact.performed += ctx => {
            if(currentInteractableObject != null){
                print("Interacted " + currentInteractableObject.gameObject.name);
                currentInteractableObject.Interacted();
            }
        };
        move.Enable();
        interact.Enable();
    }
    private void OnDisable() {
        move.Disable();
        interact.Disable();
    }
    public void Interact(){
        if(currentInteractableObject != null){
            uiInteract.SetActive(true);
        }
        else{
            uiInteract.SetActive(false);
        }
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out hit, 1.5f,LayerMask.GetMask("Interactable"))){
            InteractableObject detectedInteractableObject = hit.collider.GetComponent<InteractableObject>();
            if(detectedInteractableObject != null && detectedInteractableObject.isInteractable){
                currentInteractableObject = detectedInteractableObject;
                currentInteractableObject.EnableOutline();
            }
        }
        else{
            if(currentInteractableObject != null){
                currentInteractableObject.DisableOutline();
                currentInteractableObject = null;
            }
        }
    }
    
}
