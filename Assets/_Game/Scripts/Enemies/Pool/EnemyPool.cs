using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int initialSize = 20;

    private Queue<GameObject> availableEnemies;

    private void Awake()
    {
        availableEnemies = new Queue<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            GameObject enemy = Instantiate(
                enemyPrefab,
                transform
            );

            enemy.SetActive(false);

            EnemyHealth enemyHealth =
                enemy.GetComponent<EnemyHealth>();

            enemyHealth.OnDied += Return;

            availableEnemies.Enqueue(enemy);
        }
    }

    public GameObject Get()
    {
        if (availableEnemies.Count == 0)
        {
            Debug.LogWarning("Enemy Pool is empty.");
            return null;
        }

        GameObject enemy = availableEnemies.Dequeue();

        EnemyHealth enemyHealth =
            enemy.GetComponent<EnemyHealth>();

        enemyHealth.ResetHealth();

        enemy.SetActive(true);

       

        return enemy;
    }
    public void Return(GameObject enemy)
    {
        enemy.SetActive(false);

        enemy.transform.SetParent(transform);

        availableEnemies.Enqueue(enemy);

        
    }
}