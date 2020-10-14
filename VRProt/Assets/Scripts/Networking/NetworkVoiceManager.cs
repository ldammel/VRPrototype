using Photon.Voice.PUN;
using Photon.Voice.Unity;
using UnityEngine;

namespace Networking
{
    [RequireComponent(typeof(VoiceConnection))]
    public class NetworkVoiceManager : MonoBehaviour
    {
        public Transform remoteVoiceParent;

        private PhotonVoiceNetwork voiceConnection;

        private void Awake()
        {
            voiceConnection = GetComponent<PhotonVoiceNetwork>();
        }

        private void OnEnable()
        {
            voiceConnection.SpeakerLinked += this.OnSpeakerCreated;
        }

        private void OnDisable()
        {
            voiceConnection.SpeakerLinked -= this.OnSpeakerCreated;
        }

        private void OnSpeakerCreated(Speaker speaker)
        {
            speaker.transform.SetParent(this.remoteVoiceParent);
            speaker.OnRemoteVoiceRemoveAction += OnRemoteVoiceRemove;
        }

        private void OnRemoteVoiceRemove(Speaker speaker)
        {
            if(speaker != null)
            {
                Destroy(speaker.gameObject);
            }
        }
    }
}
