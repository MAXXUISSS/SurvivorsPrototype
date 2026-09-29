using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Vector2 moveInput;
    
    private Vector2 movement;
    
    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;
    }
    
    public void OnMove(InputValue value)
    {
        moveInput =  value.Get<Vector2>().normalized;
        
    }
}
