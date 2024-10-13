using System;
using UnityEngine;

public  class Objective : MonoBehaviour
{   
    public bool isComplete;
    public static event Action onComplete;
    public void ObjectiveComplete(){
        onComplete?.Invoke();
        isComplete = true;
        print("Objective Complete");
    }
}
