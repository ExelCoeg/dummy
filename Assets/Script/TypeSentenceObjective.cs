using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TypeSentenceObjective : Objective
{
    public UILose uiLose;
    public UIWin uiWin;
    public Transform canvas;
    public GameObject cube;
    public bool isCountDown;
    [SerializeField] private float timer = 30f;
    private void Start() {
        FindObjectOfType<Player>().enabled = false;
        FindObjectOfType<FPSCamera>().enabled = false;
        ObjectiveManager.instance.enableCursorLock = false;
        isCountDown = true;
    }
    private void Update() {
        if(isCountDown){
            timer -= Time.deltaTime;
            FindObjectOfType<UITypeSentence>().countdownText.text = ((int) timer).ToString(); 
            if(timer <= 0){
                isCountDown = false;
            }
        }
        if(!isComplete && timer <= 0){
            ObjectiveManager.instance.UILose.Show();
            FindObjectOfType<UITypeSentence>().Hide();
            ObjectiveManager.instance.enableCursorLock = false;
            Destroy(gameObject);
        }
        
        
    }
    private void ShowUIWin(){
        isCountDown = false;
        FindObjectOfType<UITypeSentence>().Hide();
        ObjectiveManager.instance.UIWin.Show();
        FindObjectOfType<Player>().enabled = true;
        FindObjectOfType<FPSCamera>().enabled = true;
        ObjectiveManager.instance.enableCursorLock = true;
        // Destroy(gameObject);
    }
    private void OnEnable() {
        onComplete += ShowUIWin;
        onComplete += SpawnCube;
    }

    private void OnDisable() {
        onComplete -= ShowUIWin;
        onComplete -= SpawnCube;
    }
    private void SpawnCube(){
        Instantiate(cube);
    }
}
