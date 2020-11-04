using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float speed;                //Floating point variable to store the player's movement speed.

    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.

    public float jumpForce;

    private float currentMomentum;

    bool isGrounded = false;

    public Transform isGroundedChecker;

    public float checkGroundRadius;

    public LayerMask groundLayer;

    public float fallMultiplier = 2.5f;

    public float lowJumpMultiplier = 2f;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
        CheckIfGrounded();
        BetterJump();
    }

    void FixedUpdate()
    {

    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }    
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb2d.velocity = new Vector2(moveBy, rb2d.velocity.y);
    }
    //Creates an empty object with a Collider2D called collider, the value of it is going to be whatever "OverlapCircle" returns.
    //When calling "OverlapCircle" which checks to see if the ground is within a certain radius of the players feet, we provide it
    //with position, radius, and layer mask which we already have.
    void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position,
        checkGroundRadius, groundLayer);
        //Checks if the circle collided with something.
        if(collider != null) 
        {
            isGrounded = true;

        }
        else
        {
            isGrounded = false;
        }
    }
    //Hold space for longer gives you a longer jump, hold it for less is shorter jump
    void BetterJump()
    {
        if (rb2d.velocity.y < 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
