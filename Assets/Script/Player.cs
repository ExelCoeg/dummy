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
    public GameObject uiInteract;
    [SerializeField] private float currentSpeed = 5f;
    // Start is called before the first frame update
   private void Awake() {
        playerInputActions = new PlayerInputActions();
   }
    private void FixedUpdate() {
        moveDirection = move.ReadValue<Vector3>();// movement
        moveDirection = moveDirection.x * transform.right + moveDirection.z * transform.forward;
        transform.position += moveDirection * currentSpeed * Time.fixedDeltaTime;
    }

    private void OnEnable(){
        
        move = playerInputActions.Player.Move;
        interact = playerInputActions.Player.Interact;
        
        
        move.Enable();
        interact.Enable();
    }
    private void OnDisable() {
        move.Disable();
        interact.Disable();
    }
   
    
}
