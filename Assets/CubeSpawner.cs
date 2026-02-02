using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;    
    public Transform player; 
    public float spawnRadius = 5f;   
    public float spawnInterval = 2f; 

    private float timer;

    void Update()
    {
        if (player == null || cubePrefab == null) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnCube();
            timer = 0f;
        }
    }

    void SpawnCube()
    {
        Vector2 randomPos = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = new Vector3(player.position.x + randomPos.x, player.position.y, player.position.z + randomPos.y);

        Instantiate(cubePrefab, spawnPos, Quaternion.identity);
    }
}
