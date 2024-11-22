using UnityEngine;
using TMPro;
using System.Threading.Tasks;
public class UISentence : MonoBehaviour
{
    public static UISentence instance;
    public TextMeshProUGUI sentenceText;
    public TextMeshProUGUI themeText;
    public TextMeshProUGUI responseText;
    public string theme;
    public string sentence;
    public async void Start() {
        await UpdateSentence();
    }
    public void SetSentence(string sentence)
    {
        sentenceText.text = sentence;
    }
    public void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    public async Task UpdateSentence(){
        theme = AIManager.instance.GetTheme();
        themeText.text = "Theme: " + theme; 
        sentence = await AIManager.instance.SendDataToGAS("make 2 sentence about " + theme);
        SetSentence(sentence);
    }
}
