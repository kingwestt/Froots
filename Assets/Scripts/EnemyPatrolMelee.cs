

using UnityEngine;

public class EnemyPatrolMelee : MonoBehaviour
{ 
        private new Rigidbody2D rigidbody;
     private Vector2 initialPosition;
         public  LayerMask mask ;
public float attackRange = 3f;
private bool Attacking= false ;
    [SerializeField] private float attackCooldown;
    private float cooldownTimer = Mathf.Infinity;

    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    [Header("Target")]
    public Transform player ;
    [Header("Edges")]
     public float LEdge;
     public float REdge;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;
    private int direction=1;

    private void Awake()
    {
        initScale = enemy.localScale;
    }
  
    private void Start()
    {
        
        rigidbody = GetComponent<Rigidbody2D>();
        initialPosition = rigidbody.position;
        anim= GetComponent<Animator>();
    }
    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void FixedUpdate()
        { Debug.Log(Attacking);
            if(!Attacking){
                if (movingLeft)
                {
                    if (enemy.position.x >= leftEdge.position.x+LEdge )
                        MoveInDirection(-1);
                    else
                        DirectionChange();
                }
                else
                {
                    if (enemy.position.x <= rightEdge.position.x-REdge)
                        MoveInDirection(1);
                    else
                        DirectionChange();
                }
        }
         float  directionToPlayer =Vector2.Distance(player.position, transform.position) ;
    //Debug.Log(directionToPlayer);
        float distance = Vector2.Distance(initialPosition, rigidbody.position);
        if (directionToPlayer < attackRange)
        {
            Debug.Log("here");
            // Check if there is an obstacle between the entity and the player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(-direction,0), attackRange,mask);
            //      Debug.Log(hit.collider.name);

            if (hit.collider != null && hit.collider.transform == player)
            {  Attacking=true;
                cooldownTimer += Time.deltaTime;
                if (cooldownTimer >= attackCooldown)
                    {
                     cooldownTimer = 0;
                    anim.SetBool("idle",false);

                    anim.SetBool("Attack",true);
                    
                    Debug.Log("attack");
                }
            }
            else
            { 
                Debug.Log(hit.collider);
                Attacking=false;
                anim.SetBool("Attack",false);
            }

        }  
            //         else
            // { Attacking=false;
            //     anim.SetBool("Attack",false);
            // }

    
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);
        
        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration )
            movingLeft = !movingLeft;
        direction =movingLeft?1:-1;
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("idle", true);
        //Make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);

        //Move in that direction

            enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z); 
    }
    
}