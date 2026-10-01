using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab ;
    [SerializeField] private float spawnCooldown = 2f;
    
    [SerializeField] private Transform player;
    [SerializeField] private float spawnDistance = 10f;
    
    private float spawnTimer;
    
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        
        if (spawnTimer > 0)
        {
            return;
        }
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition =
            player.position + (Vector3)randomDirection * spawnDistance;
        GameObject enemy = Instantiate(
            enemyPrefab,
            player.position + spawnPosition * spawnDistance,
            Quaternion.identity
        );
 
        
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        enemyMovement.SetTarget(player);
       
        
        spawnTimer = spawnCooldown;
    }
}
