using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Bullet : MonoBehaviour
{
        [SerializeField] private float speed = 40;
        [SerializeField] private float lifetime = 2;
        private Player owner;
        private double creationTime;
        private Vector3 startPosition;

        public void SetOwner(Player player)
        {
                owner = player;
        }

        public void SetCreationTime(double time)
        {
                creationTime = time;
        }
        
        public void SetStartPosition(Vector3 position)
        {
                startPosition = position;
        }

        private void Update()
        {
                float timePassed = (float) (PhotonNetwork.Time - creationTime);
                transform.position = startPosition + timePassed * speed * transform.forward;
                
                if(timePassed > lifetime) Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
                if (other.collider.CompareTag("Player"))
                {
                        var player = other.collider.GetComponent<GetParent>().parent.GetComponent<PhotonView>();
                        if(!Equals(player.Owner, owner)) player.RPC("UpdatePlayer", RpcTarget.All, new object[]{player});
                }
        }

        [PunRPC]
        public void UpdatePlayer(PhotonView view)
        { 
                Helper.SetCustomProperty(view,"IsDead",true,true);
        }
}
