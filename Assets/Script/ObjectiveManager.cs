using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager instance;
    public bool enableCursor = true;
    public bool enableCursorLock = true;
    public UITypeSentence UITypeSentence;
    public UIWin UIWin;
    public UILose UILose;
    public UIWin UIWinPrefab;
    public UILose UILosePrefab;
    public Transform canvas;
    public List<Objective> objectivesList;

    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    private void Start() {
        UIWin = Instantiate(UIWinPrefab,canvas);
        UILose = Instantiate(UILosePrefab,canvas);
        UIWin.Hide();
        UILose.Hide();
    }
    private void Update() {

        if(enableCursor){
            Cursor.visible = true;
        }
        else{
            Cursor.visible = false;
        }
        if(enableCursorLock){
            Cursor.lockState = CursorLockMode.Locked;
            
        }
        else{
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void Init(){
        Instantiate(objectivesList[0],transform);
        UITypeSentence = Instantiate(UITypeSentence,canvas);
    }
}
