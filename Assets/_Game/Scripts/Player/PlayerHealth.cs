using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{

    [SerializeField] private int maxHealth = 30;
    [SerializeField] private int currentHealth;
    [SerializeField] private float healPerSecond = 0;
    
    //Frame per frame regeneration
    private float healAccumulator;


    private void Awake()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        
        Debug.Log(
            "Health: " + currentHealth +
            " / " + maxHealth +
            " | Heal/sec: " + healPerSecond
        );
        if (currentHealth >= maxHealth)
        {
            healAccumulator = 0f;
            return;
        }

        if (healPerSecond <= 0f)
        {
            return;
        }
        // Accumulates fractional healing between frames
        healAccumulator += healPerSecond * Time.deltaTime;

        if (healAccumulator >= 1f)
        {
            int healthToRestore = Mathf.FloorToInt(healAccumulator);

            currentHealth += healthToRestore;
            Debug.Log("Player healed: +" + healthToRestore);

            currentHealth = Mathf.Min(
                currentHealth,
                maxHealth
            );

            healAccumulator -= healthToRestore;
        }
    }
    
    public void TakeDamage(int damage)
    {
        if (currentHealth == 0)
        {
            return;
        }

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player Died");
        }
    }
    public void IncreaseMaxHealth(int amount)
    {
        if (amount <= 0)
            return;

        int previousHealth = maxHealth;

        maxHealth += amount;

        Debug.Log(
            "Max Health: " +
            previousHealth +
            " → " +
            maxHealth
        );
    }
    public void IncreaseHealPerSecond(float amount)
    {
        if (amount <= 0)
            return;

        healPerSecond += amount;
    }
}
