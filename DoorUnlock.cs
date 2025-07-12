using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    public GameObject keyObject; // Assign the key in the Inspector
    public GameObject firePrefab; // Assign the fire prefab
    public Transform player; // Assign the player transform
    public float fireSpawnDistance = 2f; // Distance in front of the player

    private bool gameCompleted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!gameCompleted && other.gameObject == keyObject)
        {
            Debug.Log("GAME COMPLETED! You have saved the world by unveiling the secret history and escaping the room!");
            SpawnFire();
            gameCompleted = true; // Prevents multiple triggers
        }
    }

    private void SpawnFire()
    {
        if (firePrefab != null && player != null)
        {
            Vector3 spawnPosition = player.position + player.forward * fireSpawnDistance;
            Instantiate(firePrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("FirePrefab or Player is not assigned!");
        }
    }
}