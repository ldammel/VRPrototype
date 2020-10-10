using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Bullet : MonoBehaviour
{
        [SerializeField] private float speed = 40;
        [SerializeField] private float lifetime = 3;
        public Player owner;
        private Vector3 startPosition;
        private Rigidbody rb;
        private Renderer rend;
        private Collider col;
        private float curTime;

        private void Start()
        {
                rb = GetComponent<Rigidbody>();
                rend = GetComponent<Renderer>();
                col = GetComponent<Collider>();
        }

        private void Update()
        {
                rb.velocity = speed * transform.forward;
                curTime += Time.deltaTime;
                if (curTime < lifetime)return;
                curTime = 0;
                PhotonNetwork.Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
                if (other.collider.CompareTag("Player")) return;
                rend.enabled = false;
                col.enabled = false;
        }
        
        
}
