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

    private PhotonView weapon;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Weapon"))
        {
            weapon = other.collider.gameObject.GetComponent<PhotonView>();
            PhotonNetwork.Destroy(other.gameObject);
            if (weapon == null)
            {
                DeveloperConsole.Instance.AddLine("No Shooting Player assigned!");
                return;
            }
            DeveloperConsole.Instance.AddLine(view.gameObject.name + " got hit");
            if (Equals(weapon.ViewID, view.ViewID)) return;
            
            onHit.Invoke();
            PlayerAttributes attr = view.gameObject.GetComponent<PlayerAttributes>();
            attr.isDead = true;
            Helper.SetCustomProperty(view,"IsDead",true,true);
            DeveloperConsole.Instance.AddLine("Updated Status");
            
            if (!instantiate) return;
            var obj = Instantiate(spawnableObject, transform);
            Destroy(obj,4);
        }
    }
}
