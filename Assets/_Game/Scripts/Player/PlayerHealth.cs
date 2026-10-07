using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{

    [SerializeField] private int maxHealth = 30;
    [SerializeField] private int currentHealth;


    private void Awake()
    {
        currentHealth = maxHealth;
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
}
