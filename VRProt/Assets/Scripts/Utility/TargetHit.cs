using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class TargetHit : MonoBehaviour
{
    public PhotonView view;
    [SerializeField] private bool instantiate;
    [SerializeField] private GameObject spawnableObject;
    public UnityEvent onHit;
    
    /*private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Weapon"))
        {
            var weapon = other.collider.GetComponent<PhotonView>();
            if (Equals(weapon.Owner, view.Owner)) return;
            onHit.Invoke();
            if(view.IsMine)view.RPC("UpdatePlayer", RpcTarget.All, new object[]{view});
            
            if (!instantiate) return;
            var obj = Instantiate(spawnableObject, transform);
            Destroy(obj,4);
        }
    }*/
}
