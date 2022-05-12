/*
This script manages every aspect of the player and what they can do from running and jumping, to dying and respawning
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask platformlm;
    //Trackers
    public int maxHealth = 100;
    private int health; 
    public Healthbar healthbar;
    public int lives = 10;
    public lifeCounter lifecounter;
    public float time;
    public TimeCounter timecounter;
    public float distance;
    private bool alive = true;
    //Movement 
    public float speed;
    private float Move;
    //Jump variables
    public float jump;
    public float fallSpeed = 2.5f;
    //Coyote time variables
    public float hangTime = .2f; 
    private float hangCounter;
    //Jump buffer
    public float jumpBufferLength = .1f;
    private float jumpBufferCounter;
    //bool for flip
    bool facingRight = true;
    //components
    private Rigidbody2D rb;
    private CircleCollider2D cc;
    private Vector3 spawnPoint;
    //sounds
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource respawnSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource hitSound;

    // Start is called before the first frame update
    void Start()
    {
        //get all components and set up variables
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        lifecounter.setCounter(""+lives);
        spawnPoint = transform.position;
        fallSpeed = (fallSpeed - 1) * Physics2D.gravity.y;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * 1000;
        timecounter.formatTime(time);
        //Horizontal controls
        Move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed *Move, rb.velocity.y);

    
        if(isGrounded()) //check if the player is grounded above all else when it comes to jumping
        {
            hangCounter = hangTime; //Gives a little time after the user left the platform to jump
        } else if (rb.velocity.y > 0)
        {
            hangCounter = 0;
        } 
        else
        {
            hangCounter -= Time.deltaTime;
        }

        //allows the player to jump if the key was pressed slightly before actually reaching the ground
        if(Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferLength;
        } else  
        {
            jumpBufferCounter -= Time.deltaTime;
        }


        //Jumping controls
        if (jumpBufferCounter >=0 && hangCounter > 0)
        {
            if (rb.velocity.y < 0) //offsets the player's downward velocity
            {
                rb.AddForce(new Vector2(rb.velocity.x, -50 *rb.velocity.y));
            }
            jumpSound.Play();
            rb.AddForce(new Vector2(rb.velocity.x, jump));
            jumpBufferCounter = 0;
        }
        //Allows for snappier jumps by adding more gravity as the player falls
        if (rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + (fallSpeed * Time.deltaTime));
        }
        //Allows for shorter hops
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        //flips character sprite
        if (Input.GetAxisRaw("Horizontal") < 0 && facingRight)
        {
            flip();
        }
        if (Input.GetAxisRaw("Horizontal") > 0 && !facingRight)
        {
            flip();
        }

    }
    //uses a box cast to check if the player is touching the ground and returns whether or not the player is on a platform
    private bool isGrounded()
    {
        RaycastHit2D rhit = Physics2D.BoxCast(cc.bounds.center,cc.bounds.size,0f, Vector2.down,cc.radius + .01f, platformlm);
        return rhit.collider != null;
    }

    //if the player reaches a checkpoint refill their health and set the checkpoint as their new respawn point
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            spawnPoint = transform.position;
            health = maxHealth;
            healthbar.SetHealth(health);
        }
    }
    //Flips the entity's sprite
    private void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }
    //Subtract damage from the player's current health and change the health bar to reflect this
    public void TakeDamage (int damage)
    {
        health -= damage;
        healthbar.SetHealth(health);
        if (health <= 0 && alive)
        {
            alive = false; //check that the player is no longer alive so they take anymore damage and so the die call doesn't occur more than once at a time
            die();
        }
        hitSound.Play();
    }

    //Changes the player's position to their respawn position, refills their health, updates the healthbar, and marks the alive flag true again
    //Note this only affects the player and has no effect on enemy health or any environmental effects relying on time
    private void respawn()
    {
        transform.position = spawnPoint;
        rb.bodyType = RigidbodyType2D.Dynamic;
        health = maxHealth;
        healthbar.SetHealth(health);
        alive = true;
        respawnSound.Play();

    }

    //Stops the player from moving, subract from the life total and invoke respawn if there are lives remaining
    private void die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        //insert death animation here
        deathSound.Play();
        lives -= 1;
        lifecounter.setCounter(""+lives);
        if (lives > 0)
        {
            Invoke("respawn",.3f);
        }
        else{ //if the player has no lives remaining, marks their scores on the scoreHolder class and switch to the game over screen to let the user no their run is over
            distance = (transform.position.y + 8.5f) * 4.23f;
            ScoreHolder.setDistance(distance);
            ScoreHolder.setTime(time);
            SceneManager.LoadScene("Game_Over!");
        }
    }
}
