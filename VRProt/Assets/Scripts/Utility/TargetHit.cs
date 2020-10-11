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
            var attr = view.gameObject.GetComponent<PlayerAttributes>();
            PhotonNetwork.Destroy(other.gameObject);
            if (weapon == null)
            {
                DeveloperConsole.Instance.AddLine("No Shooting Player assigned!");
                return;
            }
            if (attr.isDead)
            {
                Respawn.Instance.DeathSpawn(view.gameObject);    
                return;
            }
            DeveloperConsole.Instance.AddLine(attr.MyName + " got hit");
            if (Equals(weapon.ViewID, view.ViewID)) return;
            onHit.Invoke();
            var instVector = transform;
            attr.isDead = true;
            attr.SetPlayerInfo(attr.Player, attr.ColorInt, attr.MyName);
            Helper.SetCustomProperty(view,"IsDead",true,true);
            DeveloperConsole.Instance.AddLine("Updated Status");
            
            if (!instantiate) return;
            PhotonNetwork.Instantiate(spawnableObject.name, instVector.position, instVector.rotation);
        }
    }
}
