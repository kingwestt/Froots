using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//to do list
//add physic material to player
//edit project input manager and add jump and crouch
//add crouch disable collider
//add floor check
//add ceiling check
//add onLandEvent

//future
//add wall check
//add wall jump
//add double jump
//add dash
//add air dash
//add sprint
//add slide
//add slide jump
//add climb
//add ciling climb



public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public float runSpeed = 40f;
    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            
        }
        if (Input.GetButtonDown("Crouch"))//maybe add codition to unlock crouch
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
        UpdateAnimator();
    }
    private void UpdateAnimator()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetBool("Crouch", crouch);
    }
    public void OnLanding()
    {
        animator.SetBool("Jump", false);
    }
    public void OnJumping()
    {
        animator.SetBool("Jump", true);
    }
    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("Crouch", isCrouching);
    }
    public void OnVerticalVelocityChange(float velocity)
    {
        animator.SetFloat("VerticalVelocity", velocity);
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove, crouch, jump);
        jump = false;
    }
}
