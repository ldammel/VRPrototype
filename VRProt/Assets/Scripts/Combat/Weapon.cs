using System;
using Photon.Pun;
using UnityEngine;

namespace Combat
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private PhotonView view;
        public PhotonView View => view;
    }
}
