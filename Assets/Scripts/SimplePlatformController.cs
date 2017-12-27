using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlatformController : MonoBehaviour
{
    public delegate void DeathAction();
    public event DeathAction OnDeath;

    [HideInInspector] public bool jump = false;
    public Transform groundCheck;
    public Transform obstacleCheck;
    public bool isBottomHero = true;

    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;

    private const float moveForce = 365f;
    private const float maxSpeed = 5f;
    private const float jumpForce = 1000f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update ()
	{

	    grounded = IsGrounded();
        
        Debug.DrawRay(transform.position, groundCheck.position);
        if (JumpInputOn() && grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);

        if (jump)
        {
            anim.SetTrigger("jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

        var obstacleHit = Physics2D.Linecast(transform.position, obstacleCheck.position,
            1 << LayerMask.NameToLayer("Obstacles"));

        if (obstacleHit)
        {
            OnDeath(); 
        }
    }

    bool IsGrounded()
    {
        bool answer;
        if (isBottomHero)
            answer = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")); 
        else
            answer = Physics2D.Linecast(transform.position, groundCheck.position,
                1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Bottom Player"));
        return answer; 
    }

    bool JumpInputOn()
    {
        if (isBottomHero && Input.GetButtonDown("Jump"))
            return true;
        if (!isBottomHero && Input.GetButtonDown("Fire1"))
            return true;
        return false; 
    }

}
