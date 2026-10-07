using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private int damage = 10;
    [SerializeField] private LayerMask enemyLayer;
    
    [SerializeField] private float attackInterval = 1f;
    private float attackTimer;
    private void Update()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer > 0)
        {
            return;
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            attackRange,
            enemyLayer
        );

        
        Collider2D closestEnemy = null;
        float closestDistance = float.MaxValue;

      
        foreach (Collider2D hit in hits)
        {
            float distance = Vector2.Distance(transform.position, hit.transform.position);

            if (distance < closestDistance)
            {
                closestEnemy = hit;
                closestDistance = distance;
            }
        }

        if (closestEnemy != null)
        {
            IDamageable damageable = closestEnemy.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                attackTimer = attackInterval;
                Debug.Log("Player dealt " + damage + " to Enemy");
            }
            
        }
    }
    
    public void IncreaseDamage(int amount)
    {
        damage += amount;
        Debug.Log("Damage: " + damage);
    }
    
    public void IncreaseAttackSpeed(float amount)
    {
        float previousInterval = attackInterval;

        attackInterval = Mathf.Max(0.1f, attackInterval - amount);

        Debug.Log(
            "Attack Interval: " + previousInterval +
            " → " + attackInterval
        );
    }
}