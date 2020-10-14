using System.Linq;
using UnityEngine;

namespace UI
{
    public class SpeakingIcon : MonoBehaviour
    {
        public AudioSource source;
        public GameObject imageHead;
        public GameObject imageHand;

        private const float UpdateStep = 0.2f;
        private const int SampleDataLength = 1024;
        private float currentUpdateTime = 0f;
        private float[] clipSampleData;

        private void Start()
        {
            clipSampleData = new float[SampleDataLength];
        }

        private void Update()
        {
            if (!source) return;
            currentUpdateTime += Time.deltaTime;
            if (!(currentUpdateTime >= UpdateStep)) return;
            currentUpdateTime = 0f;
            UpdateAudioState();
        }

        private void UpdateAudioState()
        {
            if (!source.clip) return;
            source.clip.GetData(clipSampleData, source.timeSamples);
            var clipLoudness = clipSampleData.Sum(Mathf.Abs);

            clipLoudness /= SampleDataLength;

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
