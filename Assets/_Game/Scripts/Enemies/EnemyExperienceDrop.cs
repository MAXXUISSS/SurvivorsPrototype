using UnityEngine;

public class EnemyExperienceDrop : MonoBehaviour
{
    private EnemyHealth enemyHealth;

    [SerializeField] private GameObject experienceOrbPrefab;
    [SerializeField] private int experienceReward = 1;

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

    private void DropExperience(GameObject enemy)
    {
        GameObject orb = Instantiate(
            experienceOrbPrefab,
            transform.position,
            Quaternion.identity
        );

        ExperienceOrb experienceOrb =
            orb.GetComponent<ExperienceOrb>();

        experienceOrb.SetExperienceAmount(experienceReward);

        Debug.Log("Enemy dropped experience");
    }
}