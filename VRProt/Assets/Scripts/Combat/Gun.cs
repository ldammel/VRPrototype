using Photon.Pun;
using UnityEngine;

namespace Combat
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private float speed = 40;
        [SerializeField] private float shootDelay;
        [SerializeField] private GameObject bullet;
        [SerializeField] private Transform barrel;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;

        public PhotonView photonView;

        private float lastShotTime;

        public void Fire()
        {
            if (!photonView.IsMine) return;
            if (Time.realtimeSinceStartup - lastShotTime < shootDelay) return;
            CreateProjectile(barrel.position, barrel.rotation);
            audioSource.PlayOneShot(audioClip);
        }
    
        private void CreateProjectile(Vector3 position, Quaternion rotation)
        {
            lastShotTime = Time.realtimeSinceStartup;
            var obj = PhotonNetwork.Instantiate(bullet.name, position,rotation);
            var view = obj.GetComponent<Weapon>().View;
            view.TransferOwnership(photonView.Owner);
        }
    
    }
}
