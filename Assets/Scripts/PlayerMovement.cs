using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    public float speed;
    Rigidbody2D rb;
    public float jumpForce;
    bool isGrounded = true;
    public Transform groundCheck;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    private CheckpointSystem cs;
    public Animator anim;
    public AudioSource checkpointAudio;
    public AudioSource jumpAudio;
    bool checkpointReached = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cs = GameObject.FindGameObjectWithTag("CPS").GetComponent<CheckpointSystem>();
        transform.position = cs.lastCheckPoint;

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        anim.SetBool("Grounded", isGrounded);
        Move();
        Jump();
        CheckGrounded();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }

    void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            jumpAudio.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void CheckGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, checkGroundRadius, groundLayer);
        if (collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }


    // Check for floating platforms
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MovingGround"))
        {
            transform.parent = other.transform;
        }
        if (other.CompareTag("Checkpoint") || other.CompareTag("Cave"))
        {
            Physics2D.IgnoreCollision(other, GetComponent<Collider2D>());
            if (!checkpointReached)
            {
                checkpointAudio.Play();
                checkpointReached = true;
            }

        }
    }

    //Undo floating platform
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MovingGround"))
        {
            transform.parent = null;
        }
    }

}
