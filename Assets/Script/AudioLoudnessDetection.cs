using UnityEngine;

public class AudioLoudnessDetection : MonoBehaviour
{
    public int sampleWindow = 64;
    public AudioClip microphoneClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MicrophoneToAudioClip();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MicrophoneToAudioClip(){
        string microphoneName = Microphone.devices[0];
        microphoneClip = Microphone.Start(microphoneName,true,20,AudioSettings.outputSampleRate);
    }    

    public float GetLoudnessFromMicrophone(){
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);     
    }
    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip){
        int startPostion = clipPosition - sampleWindow;
        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPostion);

        float totalLoudness = 0;
        foreach (var sample in waveData)
        {
            totalLoudness += Mathf.Abs(sample);
        }
        return totalLoudness / sampleWindow;
    }
}
