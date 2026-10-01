using UnityEngine;
using System;
public class EnemyHealth : MonoBehaviour, IDamageable
{
   [SerializeField] private int maxHealth;
   
   private int currentHealth;
   
   public event Action OnDied;
   
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
            OnDied?.Invoke();
            Destroy(gameObject);
        }
    }
}
