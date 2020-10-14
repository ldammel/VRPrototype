using Photon.Pun;
using UnityEngine;

namespace Networking
{
    public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Canvas canvas;
        private GameObject spawnedPlayerPrefab;

        public void Start()
        {
            spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
            var cam = spawnedPlayerPrefab.GetComponentInChildren<Camera>();
            canvas.worldCamera = cam;
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            PhotonNetwork.Destroy(spawnedPlayerPrefab);
        }
    }
}
