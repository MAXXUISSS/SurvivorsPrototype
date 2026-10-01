using UnityEngine;

public class EnemyExperienceDrop : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    [SerializeField] private GameObject experienceOrbPrefab;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }
    private void OnEnable()
    {
        enemyHealth.OnDied += DropExperience;
    }
    private void OnDisable()
    {
        enemyHealth.OnDied -= DropExperience;
    }
    private void DropExperience()
    {
        Instantiate(
            experienceOrbPrefab,
            transform.position,
            Quaternion.identity
        );

        Debug.Log("Enemy dropped experience");
    }
}