using Unity.VisualScripting;
using UnityEngine;

public class ScaleFromMicrophone : MonoBehaviour
{
    public AudioSource source;
    public Vector3 minScale;
    public Vector3 maxScale;
    public AudioLoudnessDetection detector;
    public float loudnessSensibility = 100;
    public float threshold = 0.1f;

    [SerializeField] private bool is2D = false;
    // Update is called once per frame
    void Update()
    {
        
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;
        if(loudness < threshold) loudness = 0;

        if(!is2D)transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
        else transform.localScale = Vector2.Lerp(minScale, maxScale, loudness);
    }
}
