using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

public class TargetHit : MonoBehaviour
{
    public PhotonView view;
    [SerializeField] private bool instantiate;
    [SerializeField] private GameObject spawnableObject;
    public UnityEvent onHit;

    private Player player;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Weapon"))
        {
            if (other.collider.gameObject.GetComponent<Bullet>()) player = other.collider.gameObject.GetComponent<Bullet>().owner;
            else if (other.collider.gameObject.GetComponent<PhotonView>()) player = other.collider.gameObject.GetComponent<PhotonView>().Owner;
            Destroy(other.gameObject);
            if (player == null)
            {
                DeveloperConsole.Instance.AddLine("No Shooting Player assigned!");
                Debug.Log("No Shooting Player assigned!");
                //return;
            }
            DeveloperConsole.Instance.AddLine(view.gameObject.name + " got hit");
            //if (Equals(player, view.Owner)) return;
            
            onHit.Invoke();
            PlayerAttributes attr = view.gameObject.GetComponent<PlayerAttributes>();
            attr.isDead = true;
            view.RPC("UpdatePlayer", RpcTarget.All, new object[]{view});
            DeveloperConsole.Instance.AddLine("Updated RPC");
            
            if (!instantiate) return;
            var obj = Instantiate(spawnableObject, transform);
            Destroy(obj,4);
        }
    }
}
