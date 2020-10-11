using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject player;
    public void LoadScene(int index)
    {
        PhotonNetwork.LoadLevel(index);
    }

    public void Quit(){
        Application.Quit();
    }
    
    public void ReSpawnMe()
    {
        Respawn.Instance.ReSpawn(player);
    }

}
