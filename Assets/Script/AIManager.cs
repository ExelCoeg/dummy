
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using System;
public class AIManager : MonoBehaviour
{
    public static AIManager instance;
    public string generatedText;
    public List<string> themes;
    // Replace with your Hugging Face API token
    #region HUGGINGFACE_API_TEXT_GENERATION
    private string apiToken = "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";
    
    // URL of the Hugging Face model
    private string apiUrl = "https://api-inference.huggingface.co/models/Qwen/Qwen2.5-1.5B-Instruct"; // or any other model
    public void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    
    public string GetTheme(){
        return themes[UnityEngine.Random.Range(0, themes.Count)];
    }
    public async Task<string> GenerateText(string prompt)
    {
        var payload = new { inputs = prompt };
        string jsonPayload = JsonConvert.SerializeObject(payload);

        using UnityWebRequest request = new UnityWebRequest(apiUrl, "POST")
        {
            uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonPayload)),
            downloadHandler = new DownloadHandlerBuffer()
        };

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", $"Bearer {apiToken}");
        
        print("Sending Request....");
        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            print("Success!");
            var responses = JsonConvert.DeserializeObject<List<InferenceResponse>>(request.downloadHandler.text);
            // generatedText = responses[0].GeneratedText.Split("\n")[0];
            generatedText = responses[0].GeneratedText;
            return generatedText;
        }
        else
        {
            Debug.LogError("Request failed: " + request.error);
            return "Error: " + request.error;
        }
    }
    #endregion

    #region API_GEMINI_TEXT_GENERATION

    [SerializeField] private string gasURL = "https://script.google.com/macros/s/AKfycbztoFpZQSrBo1PawvMQA9D3oxm1txMrJVgJ9OHbGR9NJ4LANKwIRZgLiHjpw-nCV4EVzQ/exec";
    // [SerializeField] private string prompt;


    public async Task<String> SendDataToGAS(string prompt){
        WWWForm form = new WWWForm();
        form.AddField("parameter",prompt);
        UnityWebRequest www = UnityWebRequest.Post(gasURL,form);

        await www.SendWebRequest();
        
        string response = "";

        if(www.result == UnityWebRequest.Result.Success){
            response = www.downloadHandler.text;

        }
        else{
            response = "There was an error";
        }
        return response;
    }
    #endregion
}

// Class to match response JSON structure
[System.Serializable]
public class InferenceResponse
{
    [JsonProperty("generated_text")]
    public string GeneratedText;
}
