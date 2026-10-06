using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnCooldown = 2.5f;

    [SerializeField] private float spawnDistance = 10f;

    [SerializeField] private GameManager gameManager;
    
    [SerializeField] private EnemyPool enemyPool;

    private Transform player;

    private float spawnTimer;
    private float currentSpawnCooldown;

    private void OnEnable()
    {
        gameManager.OnPlayerSpawned += Initialize;
    }

    private void OnDisable()
    {
        if (gameManager != null)
        {
            gameManager.OnPlayerSpawned -= Initialize;
        }
    }

    private void Initialize(GameObject playerObject)
    {
        player = playerObject.transform;
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        spawnTimer -= Time.deltaTime;

        UpdateSpawnDifficulty();

        if (spawnTimer > 0)
        {
            return;
        }

        Vector2 randomDirection =
            Random.insideUnitCircle.normalized;

        Vector3 spawnPosition =
            player.position +
            (Vector3)randomDirection * spawnDistance;

        GameObject enemy = enemyPool.Get();

        if (enemy == null)
        {
            return;
        }

        enemy.transform.position = spawnPosition;
        enemy.transform.rotation = Quaternion.identity;

        EnemyMovement enemyMovement =
            enemy.GetComponent<EnemyMovement>();

        enemyMovement.SetTarget(player);

        spawnTimer = currentSpawnCooldown;
    }

    private void UpdateSpawnDifficulty()
    {
        if (gameManager.ElapsedTime < 30)
        {
            currentSpawnCooldown = 2f;
        }
        else if (gameManager.ElapsedTime < 60)
        {
            currentSpawnCooldown = 1.5f;
        }
        else if (gameManager.ElapsedTime < 90)
        {
            currentSpawnCooldown = 1f;
        }
        else
        {
            currentSpawnCooldown = 0.7f;
        }
    }
}