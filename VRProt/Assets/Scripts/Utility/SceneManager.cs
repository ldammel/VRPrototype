﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject player;
    public void LoadScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    public void Quit(){
        Application.Quit();
    }

    public void ReSpawnMe()
    {
        Respawn.ReSpawn(player);
    }

}