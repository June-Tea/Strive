/*
This script manages the pink platforms and makes them fall 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling_platforms : MonoBehaviour
{
    //Keeps track of where the platform spawned
    private Vector3 startingPoint;
    //Time variables to control how much time the player has before the platform falls
    //and how long before the platform respawns
    public float fallTime = 0.5f;
    public float respawnTime = 2f;
    //components
    Rigidbody2D rb;
    BoxCollider2D bc;
    // Start is called before the first frame update
    void Start()
    {
        //get components and reference the platform's starting position
        rb = GetComponent<Rigidbody2D>();
        startingPoint = transform.position;
    }

    //checks whether the platform hits the player or any other entitiy
    void OnCollisionEnter2D(Collision2D other)
    {
        //upon collision with player, call the fall function after set amount of time
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke ("Fall", fallTime);
        }
        //if the platforms hits anything else make it respawn;
        if (!other.gameObject.CompareTag("Player"))
        {
            Invoke ("Respawn", respawnTime);

        }
    }
    //removes the kinesmatic aspect, allowing the platform to be affected by gravity
    void Fall()
    {
        rb.isKinematic = false;
        
    }

    //move the platform to where it first was and make it kinesmatic again so it stays in the air
    void Respawn()
    {
        transform.position = startingPoint;
        rb.isKinematic = true;
    }
}
