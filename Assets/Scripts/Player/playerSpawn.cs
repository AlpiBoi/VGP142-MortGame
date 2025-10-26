using UnityEngine;

public class playerSpawn : MonoBehaviour
{
    [Header("Player Settings")]
    public GameObject playerPrefab;        // Assign your player prefab here
    public Transform[] spawnPoints;        // Assign 5 spawn points in the Inspector

    void Start()
    {
        SpawnPlayerAtRandomPoint();
    }

    void SpawnPlayerAtRandomPoint()
    {
        if (playerPrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Player prefab or spawn points not assigned!");
            return;
        }

        // Pick a random spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Instantiate player at chosen spawn point
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
