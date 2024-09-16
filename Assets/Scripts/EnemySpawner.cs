using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public  Transform player;  
   // public  float spawnRadius = 20f;
    private float spawnDistance = 80f;

    private void Start()
    {
        CheckAndSpawnEnemy();
    }

    private void FixedUpdate()
    {
        CheckAndSpawnEnemy();
    }

    public void CheckAndSpawnEnemy()
    {

        GameObject enemy = EnemyPooling.instance.GetObject();
        if (enemy != null && !enemy.activeInHierarchy)
        {
            Vector3 spawnPosition = GetRandomPositionNearPlayer();
               enemy.transform.position = spawnPosition;


            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
            }
                  
        }
    }

    public  Vector3 GetRandomPositionNearPlayer()
    {

        Vector3 spawnPosition = player.position + (Random.insideUnitSphere * spawnDistance);
        spawnPosition.y = player.position.y;

        return spawnPosition;
    }
}