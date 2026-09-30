using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
   [SerializeField] private int maxHealth;
   
   private int currentHealth;
   
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
            Debug.Log("Enemy Died");
            Destroy(gameObject);
        }
    }
}
