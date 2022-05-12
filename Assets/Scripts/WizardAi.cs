/*
This script controls the wizard enemy ai and allows them to patrol and shoot at the player
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAi : MonoBehaviour
{
    //creates referenceable booleans that can't be seen in the inspector
    [HideInInspector] public bool patrol;
    [HideInInspector] public bool mustFlip;
    //lets the user reference layers
    [SerializeField] private LayerMask platformlm;
    [SerializeField] private LayerMask playerLayer;
    //basic variables
    private bool facingRight = false;
    public float speed = 100, range = 50, fireDelay = 1, wait = 2;
    private float fireTime;
    //components
    private Rigidbody2D rb;
    public Transform groundCheck;
    public Transform firepoint;
    public Transform player; 
    public GameObject frostPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //changes speed to negative on spawn because the enemy sprite faces left to begin with
        speed *= -1;
        patrol = true;
        rb = GetComponent<Rigidbody2D>();
        fireTime = fireDelay; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        //if the enemy is in patrol mode check whether the wizard needs to flip
        if (patrol)
        {
            Patrol();
            mustFlip = (!Physics2D.OverlapCircle(groundCheck.position, 0.1f, platformlm) || Physics2D.OverlapCircle(firepoint.position, 0.1f, platformlm));
        }

        fireTime -= Time.deltaTime;
        //shoots a raycast representing the wizards line of sight and switches to shoot mode if player is spotted
        RaycastHit2D hit = Physics2D.Raycast(firepoint.position, firepoint.right, range);
        bool shoot = hit.collider.CompareTag("Player");

        //when in shoot mode, stop moving and fire at an interval
        if (shoot)
        {
            patrol = false;
            wait = 2;
            if(fireTime <= 0)
            {
            fire();
            fireTime = fireDelay;
            }
        } //if the player exits field of view, wait a bit and if they don't return go back on patrol
        else {
            wait -= Time.deltaTime;
            if (wait <= 0)
            {
                patrol = true;
            }
        }
        
    }
    //creates an instant of the frostbolt prefab the firepoints position and with it's orientation
    void fire(){
        Instantiate(frostPrefab, firepoint.position, (firepoint.rotation));
    }
    //flips the enemy if necessary and allows them to move while on patrol
    void Patrol()
    {
        if (mustFlip)
        {
            flip();
        }
        rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
    }

    //Flips the entity's sprite
    private void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
        speed *= -1;
    }
}
