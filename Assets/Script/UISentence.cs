using UnityEngine;
using TMPro;
public class UISentence : MonoBehaviour
{
    public TextMeshProUGUI sentenceText;
    public TextMeshProUGUI themeText;
    public string theme;
    public string sentence;
    public void Start() {
        UpdateSentence();
    }
    public void SetSentence(string sentence)
    {
        sentenceText.text = sentence;
    }

    public void UpdateSentence(){
        theme = AIManager.instance.GetTheme();
        themeText.text = "Theme: " + theme; 
        sentence = AIManager.instance.GenerateText(theme);
        SetSentence(sentence);
    }
}
