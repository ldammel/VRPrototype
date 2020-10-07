using System;
using UnityEngine;

public class SpeakingIcon : MonoBehaviour
{
    public AudioSource source;
    public GameObject imageHead;
    public GameObject imageHand;
    
    private float updateStep = 0.2f;
    private int sampleDataLength = 1024;
    private float currentUpdateTime = 0f;
    private float[] clipSampleData;

    private void Start()
    {
        clipSampleData = new float[sampleDataLength];
    }

    private void Update()
    {
        if (source == null) return;
        currentUpdateTime += Time.deltaTime;
        if (!(currentUpdateTime >= updateStep)) return;
        currentUpdateTime = 0f;
        UpdateAudioState();
    }

    private void UpdateAudioState()
    {
        if (source.clip)
        {
            source.clip.GetData(clipSampleData, source.timeSamples);
            float clipLoudness = 0;
            foreach (float sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }

            clipLoudness /= sampleDataLength;

            if (clipLoudness > 0.005 && !imageHead.activeSelf)
            {
                imageHead.SetActive(true);
                imageHand.SetActive(true);
            }
            else if (clipLoudness < 0.005 && imageHead.activeSelf)
            {
                imageHead.SetActive(false);
                imageHand.SetActive(false);
            }
        }
    }
}
