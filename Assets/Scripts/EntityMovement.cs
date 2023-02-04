using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direction = Vector2.left;

    private new Rigidbody2D rigidbody;
    private Vector2 velocity;
    private Vector2 initialPosition;
    public float maxdis ;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        initialPosition = rigidbody.position;
    }


    
    private void OnEnable()
    {
        rigidbody.WakeUp();
    }

   

    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
        

        float distance = Vector2.Distance(initialPosition, rigidbody.position);
        Debug.Log(distance);
        if (distance > maxdis)
        {
            direction.x*=-1;
            distance=0;
            initialPosition= rigidbody.position;
    
         if (direction.x > 0f) {
             transform.localEulerAngles = new Vector2(0f, 180f);
         } else if (direction.x < 0f) {
             transform.localEulerAngles = Vector2.zero;
          }
        }
    
    }


}
