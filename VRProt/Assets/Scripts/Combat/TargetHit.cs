using Networking;
using Photon.Pun;
using UI.Console;
using UnityEngine;
using UnityEngine.Events;
using Utility;

namespace Combat
{
    public class TargetHit : MonoBehaviour
    {
        public PhotonView view;
        [SerializeField] private bool instantiate;
        [SerializeField] private GameObject spawnableObject;
        public UnityEvent onHit;

        private PhotonView weapon;
    
        private void OnCollisionEnter(Collision other)
        {
            if (!other.collider.CompareTag("Weapon")) return;
            
            //Check if we have a PhotonView on the Weapon
            weapon = other.collider.gameObject.GetComponent<Weapon>().View;
            if (weapon == null)
            {
                DeveloperConsole.Instance.AddLine("No PhotonView assigned!");
                return;
            }
            
            //Check if this player is already Dead
            var attr = view.gameObject.GetComponent<PlayerAttributes>();
            if (attr.isDead)
            {
                DeveloperConsole.Instance.AddLine("Player Already Died!");
                return;
            }
            
            //Check if we own the weapon if the weapon has an owner
            if (weapon.Owner != null && Equals(weapon.Owner, view.Owner)) return; 
            
            DeveloperConsole.Instance.AddLine(attr.MyName + " got hit");

            onHit.Invoke();
            var instVector = transform; //Vector for instantiation
            attr.isDead = true;
            attr.SetPlayerInfo(attr.Player, attr.MyName);
            Helper.SetCustomProperty(view,"IsDead",true);
            DeveloperConsole.Instance.AddLine("Updated Status");
            Respawn.Instance.DeathSpawn(view.gameObject);  
            
            if (!instantiate) return;
            PhotonNetwork.Instantiate(spawnableObject.name, instVector.position, instVector.rotation);
        }
    }
}
