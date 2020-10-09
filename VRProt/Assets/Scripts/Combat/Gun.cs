using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

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
        if (PhotonNetwork.OfflineMode)
        {
            OnShot(barrel.position, barrel.rotation,photonView.Owner, new PhotonMessageInfo());
        }
        else
        {
            photonView.RPC("OnShot", RpcTarget.All, new object[]{barrel.position, barrel.rotation,photonView.Owner});
        }
        audioSource.PlayOneShot(audioClip);
    }

    [PunRPC]
    public void OnShot(Vector3 position, Quaternion rotation, Player player, PhotonMessageInfo info)
    {
        double timestamp = PhotonNetwork.Time;

        if (!PhotonNetwork.OfflineMode)
        {
            timestamp = info.SentServerTime;
        }
        CreateProjectile(position,rotation,player,timestamp);
    }

    private void CreateProjectile(Vector3 position, Quaternion rotation, Player player, double timestamp)
    {
        lastShotTime = Time.realtimeSinceStartup;
        GameObject newProjectileObject = (GameObject) Instantiate(bullet, position,rotation);
        newProjectileObject.name = "new_" + newProjectileObject.name;

        Bullet newBullet = newProjectileObject.GetComponent<Bullet>();

        newBullet.SetCreationTime(timestamp);
        newBullet.SetStartPosition(position);
        newBullet.SetOwner(player);

    }
    
}
