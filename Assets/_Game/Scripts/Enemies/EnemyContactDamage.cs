using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float damageInterval = 0.5f;
    
    private float damageTimer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            damageTimer = damageInterval;

            Debug.Log("Enemy dealt " + damage + " to Player");
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        damageTimer -= Time.fixedDeltaTime;

        if (damageTimer <= 0)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                damageTimer = damageInterval;

                Debug.Log("Enemy dealt " + damage + " to Player");
            }
        }
    }
}