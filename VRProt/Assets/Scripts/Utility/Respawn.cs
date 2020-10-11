using UnityEngine;
using Debug = UnityEngine.Debug;

public class Respawn : MonoBehaviour
{
    public static Respawn Instance;
    public Transform reSpawnPosition;
    public Transform deathSpawnPosition;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Too many Respawn prefabs in the scene");
        }
        Instance = this;
    }

    public void ReSpawn(GameObject player)
    {
        player.transform.position = reSpawnPosition.position;
    }

    public void DeathSpawn(GameObject player)
    {
        player.transform.position = deathSpawnPosition.position;
    }
}
