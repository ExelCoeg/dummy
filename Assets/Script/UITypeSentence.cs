using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITypeSentence : UI
{
    public TMP_InputField inputField;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI sentenceText;
    public Button button;
    public string sentence;
    public string answer;
    private void Start() {
        sentenceText.text = sentence;
        button.onClick.AddListener(CheckAnswer);
    }
    public void CheckAnswer(){
        if(inputField.text == sentenceText.text){
            ObjectiveManager.instance.objectivesList[0].ObjectiveComplete();
        }
    }
}
