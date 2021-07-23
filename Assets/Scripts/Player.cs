using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Sprite[] sprites;

    public Health health;
    public Animator animator;
    
    protected float immunityTime;
    protected float immunityDuration = 1.0f;
    private bool isImmune = false;
    

    public Vector3 movement;
    public AudioSource audioSource;
    private void Start()
    {
        movement = new Vector3(0, (float) -0.5, 0);
    }

    void FixedUpdate(){
        if (isImmune == true)
        {
            immunityTime = immunityTime + Time.deltaTime;
            if (immunityTime >= immunityDuration)
            {
                isImmune = false;
            }
        }
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);

        if (movement.x > 0)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[2];
        }
        if (movement.x < 0)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
        if (movement.y > 0)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[3];
        }
        if (movement.y < 0)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[0];

        }

        transform.Translate(movement * moveSpeed * Time.deltaTime, 0);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Enemy")){
            if (!isImmune)
            {
                this.immunityTime = 0;
                this.health.health--;
                this.isImmune = true;
                animator.SetTrigger("Damage");
                audioSource.Play();
                
            }
        }
    }

}
