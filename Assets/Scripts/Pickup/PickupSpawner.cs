using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [Header("Pickup Settings")]
    public GameObject pickupPrefab;   // Your SpeedPickup prefab
    public int numberOfPickups = 3;   // How many pickups to spawn

    [Header("Spawn Area")]
    public Vector3 center = Vector3.zero;  // Center of spawn area
    public Vector3 size = new Vector3(10, 1, 10); // Width, height, depth of spawn area

    void Start()
    {
        SpawnPickups();
    }

    void SpawnPickups()
    {
        if (pickupPrefab == null)
        {
            Debug.LogWarning("Pickup prefab not assigned!");
            return;
        }

        for (int i = 0; i < numberOfPickups; i++)
        {
            Vector3 randomPos = GetRandomPosition();
            Instantiate(pickupPrefab, randomPos, Quaternion.identity);
        }
    }

    Vector3 GetRandomPosition()
    {
        // Random position within the defined area
        float x = center.x + Random.Range(-size.x / 2, size.x / 2);
        float y = center.y + Random.Range(-size.y / 2, size.y / 2);
        float z = center.z + Random.Range(-size.z / 2, size.z / 2);

        return new Vector3(x, y, z);
    }

    // Optional: visualize spawn area in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Gizmos.DrawCube(center, size);
    }
}
