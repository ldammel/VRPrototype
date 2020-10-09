using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Bullet : MonoBehaviour
{
        [SerializeField] private float speed = 40;
        [SerializeField] private float lifetime = 2;
        public Player owner;
        private double creationTime;
        private Vector3 startPosition;
        private Rigidbody rb;
        private Renderer rend;
        private Collider col;

        private void Start()
        {
                rb = GetComponent<Rigidbody>();
                rend = GetComponent<Renderer>();
                col = GetComponent<Collider>();
        }

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
                rb.velocity = speed * transform.forward;
                Destroy(gameObject,lifetime);
        }

        private void OnCollisionEnter(Collision other)
        {
                if (other.collider.CompareTag("Player")) return;
                rend.enabled = false;
                col.enabled = false;
                Destroy(gameObject,2);
        }
}
