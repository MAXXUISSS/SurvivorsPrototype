using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Vector2 moveInput;
    
    private Vector2 movement;
    
    void Update()
    {
        transform.position += (Vector3)(moveInput * speed * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        moveInput =  value.Get<Vector2>().normalized;
        
    }
}
