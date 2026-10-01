using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform target;
    [SerializeField] private float stoppingDistance = 0.5f;
    
    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float playerDistance = Vector2.Distance(transform.position, target.position);

        if (playerDistance > stoppingDistance)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enemy touched something");
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }


}
