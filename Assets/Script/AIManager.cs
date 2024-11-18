
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
public class AIManager : MonoBehaviour
{
    public static AIManager instance;
    public string generatedText;
    public List<string> themes;
    // Replace with your Hugging Face API token
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
    
    // Method to call the API
    public string GenerateText(string prompt)
    {
        StartCoroutine(SendRequest(prompt));
        return generatedText;
    }
    public string GetTheme(){
        return themes[Random.Range(0, themes.Count)];
    }
    private IEnumerator SendRequest(string prompt)
    {

        // JSON payload
        var payload = new { inputs = prompt };
        string jsonPayload = JsonConvert.SerializeObject(payload);

        // Create request
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);

        request.downloadHandler = new DownloadHandlerBuffer();
        
        // Set headers
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", $"Bearer {apiToken}");

        // Send request and wait for response
        print("Sending request...");
        yield return request.SendWebRequest();  

        if (request.result == UnityWebRequest.Result.Success)
        {
            print("Success");
            // Parse the response as an array
            var responses = JsonConvert.DeserializeObject<InferenceResponse[]>(request.downloadHandler.text);
            // generatedText = responses[0].GeneratedText.Split('\n')[1];
            generatedText = responses[0].GeneratedText;
            print(generatedText);

        }
        else
        {
            Debug.LogError("Request failed: " + request.error);
        }
    }
}

// Class to match response JSON structure
[System.Serializable]
public class InferenceResponse
{
    [JsonProperty("generated_text")]
    public string GeneratedText;
}
