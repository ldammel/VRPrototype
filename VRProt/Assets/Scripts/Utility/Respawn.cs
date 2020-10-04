using System;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform reSpawnPosition;
    public static Transform ReSpawnPosition;

    private void Start()
    {
        ReSpawnPosition = reSpawnPosition;
    }

    public static void ReSpawn(GameObject player)
    {
        player.transform.position = ReSpawnPosition.position;
    }
}
