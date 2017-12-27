using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlatformController : MonoBehaviour
{

    [HideInInspector] public bool jump = false;

    public float moveForce = 365f;
    public const float maxSpeed = 5f;
    public const float jumpForce = 1000f;
    public Transform groundCheck;
    public Transform obstacleCheck;
    public bool isBottomHero = true;
    public GameObject parent; 

    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d; 

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
            parent.transform.position = new Vector3(0, parent.transform.position.y, parent.transform.position.z); 
        }
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
    //    {
    //        var parentTransform = parent.transform;
    //        parentTransform.position = new Vector3(0, parentTransform.position.y, parentTransform.position.z);
    //    }
    //}

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
