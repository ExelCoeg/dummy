using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechRecognitionTest : MonoBehaviour
{
    public KeywordRecognizer keywordRecognizer;
    public DictationRecognizer dictationRecognizer;
    public string[] unconfidenceKeywords;

    public void Start() {
        
        keywordRecognizer = new KeywordRecognizer(unconfidenceKeywords);
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        // keywordRecognizer.Start();
        StartDictactionRecognition();


    }
     private void StartDictactionRecognition()
    {
        dictationRecognizer = new DictationRecognizer();

        dictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.Log($"Recognized: {text}");
            AnalyzeSpeech(text,UISentence.instance.sentenceText.text);
        };

        dictationRecognizer.DictationHypothesis += (text) =>
        {
            Debug.Log($"Hypothesis: {text}");
        };

        dictationRecognizer.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
                Debug.LogError("Dictation completed unexpectedly: " + completionCause);
        };

        dictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogError($"Dictation error: {error}; HResult = {hresult}");
        };

        dictationRecognizer.Start();
        Debug.Log("Dictation recognizer started. Speak into the microphone.");
    }
     private void AnalyzeSpeech(string userText, string targetText)
    {
        // Calculate similarity using Levenshtein Distance
        double similarity = CalculateSimilarity(targetText, userText);
        Debug.Log($"Similarity Score: {similarity * 100}%");

        // Provide feedback based on similarity
        if (similarity > 0.9)
        {
            Debug.Log("Great job! Your speech closely matches the target text.");
        }
        else
        {
            Debug.Log("Keep practicing! Try to match the text more closely.");
        }
    }
     private double CalculateSimilarity(string target, string user)
    {
        int distance = LevenshteinDistance(target, user);
        return 1.0 - (double)distance / Mathf.Max(target.Length, user.Length);
    }

    private int LevenshteinDistance(string a, string b)
    {
        var costs = new int[b.Length + 1];
        for (int j = 0; j < costs.Length; j++)
            costs[j] = j;

        for (int i = 1; i <= a.Length; i++)
        {
            costs[0] = i;
            int nw = i - 1;

            for (int j = 1; j <= b.Length; j++)
            {
                int cj = Mathf.Min(1 + Mathf.Min(costs[j], costs[j - 1]), a[i - 1] == b[j - 1] ? nw : nw + 1);
                nw = costs[j];
                costs[j] = cj;
            }
        }
        return costs[b.Length];
    }
    private void RecognizedSpeech(PhraseRecognizedEventArgs args){
        print(args.text);
        UISentence.instance.responseText.text += " " + args.text;
    }

}
