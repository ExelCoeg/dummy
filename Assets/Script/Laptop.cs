using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laptop : InteractableObject
{
    [SerializeField] float timer = 1.5f;
    public bool startTimer = false;
    public override void Interacted()
    {
        startTimer = true;
        isInteractable = false;
    }
    private void Update() {
        if(startTimer){
            timer -= Time.deltaTime;
            if(timer <= 0){
                startTimer = false;
                ObjectiveManager.instance.Init();
                enabled = false;
            }
        }        
    }


    
}
