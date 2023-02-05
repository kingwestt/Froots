using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement1 : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direction = Vector2.left;
public Transform player;
public float attackRange = 3f;
public int damage = 10;
    private new Rigidbody2D rigidbody;
    private Vector2 velocity;
    private Vector2 initialPosition;
    public float maxdis ;
    public  LayerMask mask ;
    public bool PlayerinView=false;
    Animator anim;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        initialPosition = rigidbody.position;
        anim= GetComponent<Animator>();
    }


    
    private void OnEnable()
    {
        rigidbody.WakeUp();
    }

   

    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y = Physics2D.gravity.y * Time.fixedDeltaTime;

        rigidbody.velocity=new Vector2( velocity.x,velocity.y );
        float  directionToPlayer =Vector2.Distance(player.position, transform.position) ;
//Debug.Log(directionToPlayer);
        float distance = Vector2.Distance(initialPosition, rigidbody.position);
        RayTracker();
if(PlayerinView){
        
    

        // Check if there is an obstacle between the entity and the player
      
           //      Debug.Log(hit.collider.name);


       
            // Trigger attack animation
            // ...
            rigidbody.velocity=Vector2.zero;
            // Subtract player's health
             anim.SetBool("Attack",true);
          
            Debug.Log("attack");
        
        
    }
     else   if (distance > maxdis)
        {
                 anim.SetBool("Attack",false);
            direction.x*=-1;
            distance=0;
            initialPosition= rigidbody.position;
    
         if (direction.x > 0f) {
             transform.localEulerAngles = new Vector2(0f, 180f);
         } else if (direction.x < 0f) {
             transform.localEulerAngles = Vector2.zero;
          }
        }
    
    
          anim.SetFloat("magnitude",rigidbody.velocity.magnitude);
          Debug.Log(rigidbody.velocity.magnitude);
    }


void RayTracker()
{
    
  RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right,attackRange,mask);
         if (hit.collider != null )
        {
           
           Debug.Log("here");
            if(hit.collider.transform == player)
            {
        
            PlayerinView=true;
        }
        else
        {
           PlayerinView=false;
         }
        }
        else
        {
           PlayerinView=false;
         }
}
}
